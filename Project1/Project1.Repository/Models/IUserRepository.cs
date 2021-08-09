using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Repository.Models
{
    public interface IUserRepository
    {
        User[] GetAllUsers();
        User GetUser(int UserId);
    }
}
