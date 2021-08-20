using Project1.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business
{
    public class UserClassManager : IUserClassManager
    {
        private readonly IUserClassRepository userClassRepository;

        public UserClassManager(IUserClassRepository Repository)
        {
            userClassRepository = Repository;
        }

        public void RegisterForClass(int UserId, int ClassId)
        {
            userClassRepository.RegisterUserForClass(UserId, ClassId);
        }
    }
}
