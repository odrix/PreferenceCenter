using NUnit.Framework;
using PreferenceCenterAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenceCenterTests
{
    [TestFixture]
    internal class UserServiceTests
    {
        [Test]
        public void CreateUserAndGetByEmail()
        {
            string email = "pierre.tombal@joke.net";

            UserService userService = new UserService();
            userService.Add(email);

            var  user = userService.Get(email);

            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email); 
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.IsNotNull(user.Consents);
        }

        [Test]
        public void CreateUserAndGetById()
        {
            string email = "pierre.tombal@joke.net";

            UserService userService = new UserService();
            var createdUser = userService.Add(email);

            var user = userService.Get(createdUser.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.IsNotNull(user.Consents);
        }

        [Test]
        public void CreateAlredayExistUser_ShouldThrowArgumentExcpetion()
        {
            string email = "pierre.tombal@joke.net";

            UserService userService = new UserService();
            userService.Add(email);
            Assert.Throws<ArgumentException>(() => userService.Add(email), "email already exist.");
        }

        [Test]
        public void CreateUserWithBadEmail_ShouldThrowArgumentExcpetion()
        {
            string email = "pierre.tombal";

            UserService userService = new UserService();
            Assert.Throws<FormatException>(() => userService.Add(email), "Email is wrong formated.");
        }


        [Test]
        public void DeleteExistingUser_ShouldReturnTrue()
        {
            string email = "pierre.tombal@joke.net";

            UserService userService = new UserService();
            userService.Add(email);

            var isDeleted = userService.Delete(email);

            Assert.True(isDeleted);
        }

        [Test]
        public void DeleteUnexistingUser_ShouldReturnFalse()
        {
            string email = "pierre.tombal@joke.net";
            string unexistEmail = "dark.vador@death.star";

            UserService userService = new UserService();
            userService.Add(email);

            var isDeleted = userService.Delete(unexistEmail);

            Assert.False(isDeleted);
        }
    }
}
