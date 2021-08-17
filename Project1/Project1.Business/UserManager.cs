using Project1.Repository;
using Project1.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository UserRepository)
        {
            userRepository = UserRepository;
        }

        public UserModel[] Users
        {
            get
            {
                return userRepository.GetAllUsers()
                    .Select(u => new UserModel()
                    {
                        UserId = u.UserId,
                        UserEmail = u.UserEmail,
                        UserIsAdmin = u.UserIsAdmin,
                        UserPassword = u.UserPassword
                    })
                    .ToArray();
            }
        }

        public UserModel User(int UserId)
        {
            User user = userRepository.GetUser(UserId);

            UserModel userModel = new UserModel()
            {
                UserId = user.UserId,
                UserEmail = user.UserEmail,
                UserIsAdmin = user.UserIsAdmin,
                UserPassword = user.UserPassword,
                Classes = user.Classes
                    .Select(c => new Models.ClassModel()
                    {
                        ClassId = c.ClassId,
                        ClassName = c.ClassName,
                        ClassDescription = c.ClassDescription,
                        ClassPrice = c.ClassPrice
                    })
                    .ToList<Models.ClassModel>()
            };

            return userModel;
        }

        public UserModel User(string UserEmail)
        {
            User user = userRepository.GetUser(UserEmail);
            UserModel userModel = null;

            if (user != null)
            {
                 userModel = new UserModel()
                {
                    UserId = user.UserId,
                    UserEmail = user.UserEmail,
                    UserIsAdmin = user.UserIsAdmin,
                    UserPassword = user.UserPassword,
                    Classes = user.Classes
                        .Select(c => new Models.ClassModel()
                        {
                            ClassId = c.ClassId,
                            ClassName = c.ClassName,
                            ClassDescription = c.ClassDescription,
                            ClassPrice = c.ClassPrice
                        })
                        .ToList<Models.ClassModel>()
                };
            }

            return userModel;
        }
    
        public void RegisterUser(string UserEmail, string UserPassword)
        {
            userRepository.AddUser(UserEmail, UserPassword);
        }

        public bool LoginUser(string UserEmail, string UserPassword)
        {
            var user = userRepository.GetUser(UserEmail);

            if (user.UserPassword != UserPassword)
                return false;

            return true;
        }
    }
}
