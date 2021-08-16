using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project1.Repository.Models;

namespace Project1.Repository
{
    public class UserRepository : IUserRepository
    {
        public User[] GetAllUsers()
        {
            User[] users = DatabaseAccessor.Instance.Users
                .Select(u => new User()
                {
                    UserId = u.UserId,
                    UserEmail = u.UserEmail,
                    UserIsAdmin = u.UserIsAdmin,
                    UserPassword = u.UserPassword
                })
                .ToArray<User>();

            return users;
        }

        public User GetUser(int UserId)
        {
            User user = DatabaseAccessor.Instance.Users
                .Where(u => u.UserId == UserId)
                .Include(c => c.UserClasses)
                .Select(u => new User()
                {
                    UserId = u.UserId,
                    UserEmail = u.UserEmail,
                    UserIsAdmin = u.UserIsAdmin,
                    UserPassword = u.UserPassword,
                    Classes = u.UserClasses
                        .Where(x => x.UserId == UserId)
                        .Select(c => new Class()
                        {
                            ClassId = c.ClassId,
                            ClassName = c.Class.ClassName,
                            ClassDescription = c.Class.ClassDescription,
                            ClassPrice = c.Class.ClassPrice
                        })
                        .ToList()
                })
                .FirstOrDefault<User>();

            return user;
        }

        public User GetUser(string UserEmail)
        {
            User user = DatabaseAccessor.Instance.Users
                .Where(u => u.UserEmail == UserEmail)
                .Include(c => c.UserClasses)
                .Select(u => new User()
                {
                    UserId = u.UserId,
                    UserEmail = u.UserEmail,
                    UserIsAdmin = u.UserIsAdmin,
                    UserPassword = u.UserPassword,
                    Classes = u.UserClasses
                        .Where(x => x.UserId == u.UserId)
                        .Select(c => new Class()
                        {
                            ClassId = c.ClassId,
                            ClassName = c.Class.ClassName,
                            ClassDescription = c.Class.ClassDescription,
                            ClassPrice = c.Class.ClassPrice
                        })
                        .ToList()
                })
                .FirstOrDefault<User>();

            return user;
        }

        public void AddUser(string Email, string Password)
        {
            Database.User user = new Database.User()
            {
                UserEmail = Email,
                UserPassword = Password
            };

            DatabaseAccessor.Instance.Add(user);
            DatabaseAccessor.Instance.SaveChanges();
        }
    }
}
