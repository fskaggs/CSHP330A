using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            User user = (User)DatabaseAccessor.Instance.Users
                .Select(u => new User()
                {
                    UserId = u.UserId,
                    UserEmail = u.UserEmail,
                    UserIsAdmin = u.UserIsAdmin,
                    UserPassword = u.UserPassword
                });

            return user;
        }
    }
}
