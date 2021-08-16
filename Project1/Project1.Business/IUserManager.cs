using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business
{
    public interface IUserManager
    {
        public UserModel[] Users { get; }
        public UserModel User(int UserId);
        public UserModel User(string UserEmail);
        public void RegisterUser(string UserEmail, string UserPassword);
    }
}
