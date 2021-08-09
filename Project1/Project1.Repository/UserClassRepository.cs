using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Repository.Models;

namespace Project1.Repository
{
    public class UserClassRepository : IUserClassRepository
    {
        public Class[] GetClassesByUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsersByClass(int ClassId)
        {
            throw new NotImplementedException();
        }
    }
}
