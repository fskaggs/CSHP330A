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

        public void RegisterUserForClass(int UserId, int ClassId)
        {
            var curUserEnrollment = DatabaseAccessor.Instance.UserClasses
                .Where(uc => uc.ClassId == ClassId && uc.UserId == UserId).FirstOrDefault();

            if (curUserEnrollment == null)
            {
                Database.UserClass userClass = new Database.UserClass()
                {
                    ClassId = ClassId,
                    UserId = UserId
                };

                DatabaseAccessor.Instance.Add(userClass);
                DatabaseAccessor.Instance.SaveChanges();
            }
        }
    }
}
