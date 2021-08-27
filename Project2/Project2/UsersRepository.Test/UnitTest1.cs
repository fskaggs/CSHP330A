using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using UserRepository;
using UserRepository.Models;

namespace UsersRepository.Test
{
    public class Tests
    {
        UserRepository.UserRepository r;

        [SetUp]
        public void Setup()
        {
            r = new UserRepository.UserRepository();
        }

        [Test]
        public void GetAll_Users_Initial()
        {
            var users = r.GetAll();
            Assert.AreEqual(users.Count(), 3);
        }

        [Test]
        public void GetAll_Users()
        {
            var users = r.GetAll().Where(u => u.Email == "Fred");
            Assert.AreEqual(users.Count(), 1);
        }

        [Test]
        public void GetUserById()
        {
            var users = r.Get("efa73921-be06-4b81-ad19-3a27f1f4f07c");
            Assert.IsNotNull(users);
            Assert.AreEqual("Fred", users.Email);
            Assert.AreEqual("fred", users.Password);
        }

        [Test]
        public void AddUserTest()
        {
            string userId = "9d2e2836-74f5-4d45-b23b-73fbaed18427";

            UserModelRepo newUser = new UserModelRepo()
            {
                Id = userId,
                CreatedDate = DateTime.UtcNow,
                Email = "Test User",
                Password = "test"
            };

            r.Add(newUser);

            var checkUser = r.Get(userId);
            Assert.IsNotNull(checkUser);
            Assert.AreEqual("Test User", checkUser.Email);
            Assert.AreEqual("test", checkUser.Password);
        }

        [Test]
        public void AddDuplicateUserTest()
        {
            string userId = "015f5067-2a0d-4ce6-a688-fe6b2e9d0a4e";

            UserModelRepo newUser = new UserModelRepo()
            {
                Id = userId,
                CreatedDate = DateTime.UtcNow,
                Email = "Test User",
                Password = "test"
            };

            Assert.Throws<ArgumentException>(() => r.Add(newUser));
        }

        [Test]
        public void AddUserNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => r.Add(null));
        }

        [Test]
        public void DeleteUserTest()
        {
            var user = r.GetByName("Zoltan");
            Assert.AreEqual("Zoltan", user.Email);
            r.Delete(user.Id);
            Assert.IsNull(r.Get(user.Id));
        }

        [Test]
        public void DeleteUserNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => r.Delete(null));
        }

        [Test]
        public void UpdateUser()
        {
            UserModelRepo user = r.GetByName("Fred");
            
            user.Password = "new password";
            r.Update(user);

            UserModelRepo updatedUser = r.GetByName("Fred");
            Assert.AreEqual("new password", user.Password);
        }
    }
}