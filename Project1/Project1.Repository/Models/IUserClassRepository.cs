using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Repository.Models
{
    public interface IUserClassRepository
    {
        Class[] GetClassesByUser(int UserId);
        User[] GetUsersByClass(int ClassId);
        public void RegisterUserForClass(int UserId, int ClassId);
    }
}
