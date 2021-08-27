using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserRepository.Models;

namespace UserRepository
{
    public class UserRepository : IUserRepository<UserModelRepo>
    {
        List<UserModelRepo> data;

        public UserRepository()
        {
            data = new List<UserModelRepo>()
            {
                new UserModelRepo()
                {
                    Id = "efa73921-be06-4b81-ad19-3a27f1f4f07c",
                    Email = "Fred",
                    Password = "fred",
                    CreatedDate = DateTime.UtcNow
                },
                new UserModelRepo()
                {
                    Id = "b57954dd-21cc-4b4c-94f5-4307f4893ab5",
                    Email = "Joe",
                    Password = "Joe",
                    CreatedDate = DateTime.UtcNow
                },
                new UserModelRepo()
                {
                    Id = "015f5067-2a0d-4ce6-a688-fe6b2e9d0a4e",
                    Email = "Zoltan",
                    Password = "zoltan",
                    CreatedDate = DateTime.UtcNow
                }
            };
        }

        public void Add(UserModelRepo Entity)
        {
            if (Entity == null)
                throw new ArgumentNullException("User repository argument Entity is null on Add operation.");

            if (Get(Entity.Id) != null)
                throw new ArgumentException("Attempt to Add Entity already present in DB");

            data.Add(Entity);
            return;
        }

        public void Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id) == true)
                throw new ArgumentNullException("User repository argument Entity is null on Delete operation.");

            data.RemoveAll(x => x.Id == Id);
        }

        public IEnumerable<UserModelRepo> FindByCondition(Func<UserModelRepo, bool> Ex)
        {
            var result = (IEnumerable<UserModelRepo>)data.Where(Ex).ToList();
            return result;
        }

        public UserModelRepo Get(string Id)
        {
            if (String.IsNullOrEmpty(Id) == true)
                throw new ArgumentNullException("User repository argument Entity is null on Get operation.");

            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<UserModelRepo> GetAll()
        {
            return FindByCondition(u => true).ToList();
        }

        public UserModelRepo GetByName(string Email)
        {
            if (Email == null)
                throw new ArgumentNullException("User repository argument Entity is null on GetByName operation.");

            return FindByCondition(x => x.Email == Email).FirstOrDefault();
        }

        public void Update(UserModelRepo Entity)
        {
            UserModelRepo entityToUpdate = data.Where<UserModelRepo>(x => x.Id == Entity.Id).FirstOrDefault();

            if (entityToUpdate == null)
                throw new NullReferenceException("Attempt to update entity which does not exist");

            entityToUpdate.Email = Entity.Email;
            entityToUpdate.Password = Entity.Password;
            entityToUpdate.CreatedDate = Entity.CreatedDate;
        }
    }
}
