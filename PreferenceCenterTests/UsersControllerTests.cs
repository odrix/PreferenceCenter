using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NUnit.Framework;
using PreferenceCenterAPI.Controllers;
using PreferenceCenterAPI.Domain;
using System;

namespace PreferenceCenterTests
{
    [TestFixture]
    internal class UsersControllerTests
    {
        [Test]
        public void CreateUserAndGetByEmail()
        {
            string email = "pierre.tombal@joke.net";
            UsersController userCtrl = InitUserController();
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
        public void GetUnexistingId_ShouldResponseNotFound()
        {
            UsersController userCtrl = InitUserController();

            var response = userCtrl.Get(Guid.NewGuid());

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void CreateAlredayExistUser_ShouldResponseBadRequest()
        {
            string email = "pierre.tombal@joke.net";

            UsersController usersCtrl = InitUserController();
            usersCtrl.Post(email);
            var response = usersCtrl.Post(email);

            Assert.IsInstanceOf<IStatusCodeActionResult>(response);
            Assert.AreEqual(422, (response as IStatusCodeActionResult).StatusCode);
        }

        [Test]
        public void CreateUserWithBadEmail_ShouldResponseBadRequest()
        {
            string email = "pierre.tombal";

            UsersController usersCtrl = InitUserController();
            var response = usersCtrl.Post(email);

            Assert.IsInstanceOf<IStatusCodeActionResult>(response);
            Assert.AreEqual(422, (response as IStatusCodeActionResult).StatusCode); 
        }

        [Test]
        public void DeleteExistingUser_ShouldResponseOk()
        {
            string email = "pierre.tombal@joke.net";

            UsersController userCtrl = InitUserController();
            userCtrl.Post(email);

            var response = userCtrl.Delete(email);

            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public void DeleteUnexistingUser_ShouldResponseNoContent()
        {
            string email = "pierre.tombal@joke.net";

            UsersController userCtrl = InitUserController();
            userCtrl.Post(email);

            var response = userCtrl.Delete(Guid.NewGuid());

            Assert.IsInstanceOf<NoContentResult>(response);
        }

        private static UsersController InitUserController()
        {
            return new UsersController(new UserService(new InMemoryContext()));
        }
    }
}
