using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PreferenceCenterAPI.Controllers;
using PreferenceCenterAPI.Models;
using PreferenceCenterAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenceCenterTests
{
    [TestFixture]
    internal class UsersControllerTests
    {
        [Test]
        public void CreateUserAndGetByEmail()
        {
            string email = "pierre.tombal@joke.net";

            UsersController userCtrl = new UsersController(new UserService());
            userCtrl.Post(email);

            var response = userCtrl.Get(email);

            Assert.IsInstanceOf<JsonResult>(response);

            var user = (response as JsonResult).Value as UserPreference;
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.IsNotNull(user.Consents);
        }

        [Test]
        public void CreateAlredayExistUser_ShouldThrowArgumentExcpetion()
        {
            string email = "pierre.tombal@joke.net";

            UsersController usersCtrl = new UsersController(new UserService());
            usersCtrl.Post(email);
            var response = usersCtrl.Post(email);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public void CreateUserWithBadEmail_ShouldThrowArgumentExcpetion()
        {
            string email = "pierre.tombal";

            UsersController usersCtrl = new UsersController(new UserService());
            var response = usersCtrl.Post(email);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }
    }
}
