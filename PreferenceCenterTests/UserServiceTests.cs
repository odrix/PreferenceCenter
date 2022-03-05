using NUnit.Framework;
using PreferenceCenterAPI.Domain;
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

            UserService userService = new UserService(new InMemoryContext());
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

            UserService userService = new UserService(new InMemoryContext());
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

            UserService userService = new UserService(new InMemoryContext());
            userService.Add(email);
            Assert.Throws<ArgumentException>(() => userService.Add(email), "email already exist.");
        }

        [Test]
        public void CreateUserWithBadEmail_ShouldThrowArgumentExcpetion()
        {
            string email = "pierre.tombal";

            UserService userService = new UserService(new InMemoryContext());
            Assert.Throws<ArgumentException>(() => userService.Add(email), "Email is wrong formated.");
        }


        [Test]
        public void DeleteExistingUser_ShouldReturnTrue()
        {
            string email = "pierre.tombal@joke.net";

            UserService userService = new UserService(new InMemoryContext());
            userService.Add(email);

            var isDeleted = userService.Delete(email);

            Assert.True(isDeleted);
        }

        [Test]
        public void DeleteUnexistingUser_ShouldReturnFalse()
        {
            string email = "pierre.tombal@joke.net";
            string unexistEmail = "dark.vador@death.star";

            UserService userService = new UserService(new InMemoryContext());
            userService.Add(email);

            var isDeleted = userService.Delete(unexistEmail);

            Assert.False(isDeleted);
        }

        [Test]
        public void GetUser_ShouldReturnOnlyLastConsentsValue()
        {
            string email = "pierre.tombal@joke.net";
            var inMemoryContext = new InMemoryContext();
            Guid userId = Guid.NewGuid();   
            inMemoryContext.AddUser(new UserPreference() { Id = userId, Email=email });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.email_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.sms_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.email_notifications, Enabled = false } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.email_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.sms_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.email_notifications, Enabled = false } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.email_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId=userId, Id = EnumConsent.email_notifications, Enabled = false } });

            UserService userService = new UserService(inMemoryContext);
            var user = userService.Get(email);

            Assert.IsNotNull(user.Consents);
            Assert.AreEqual(2, user.Consents.Count);
            Assert.AreEqual(EnumConsent.email_notifications, user.Consents[0].Id);  
            Assert.False(user.Consents[0].Enabled);
            Assert.AreEqual(EnumConsent.sms_notifications, user.Consents[1].Id);
            Assert.True(user.Consents[1].Enabled);
        }

        [Test]
        public void GetUserWithOnlyEmailNotificationConsents_ShouldReturnOnlyLastConsentValue()
        {
            string email = "pierre.tombal@joke.net";
            var inMemoryContext = new InMemoryContext();
            Guid userId = Guid.NewGuid();
            inMemoryContext.AddUser(new UserPreference() { Id = userId, Email = email });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId = userId, Id = EnumConsent.email_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId = userId, Id = EnumConsent.email_notifications, Enabled = false } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId = userId, Id = EnumConsent.email_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId = userId, Id = EnumConsent.email_notifications, Enabled = false } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId = userId, Id = EnumConsent.email_notifications, Enabled = true } });
            inMemoryContext.AddEvents(new Consent[1] { new Consent { UserId = userId, Id = EnumConsent.email_notifications, Enabled = false } });

            UserService userService = new UserService(inMemoryContext);
            var user = userService.Get(email);

            Assert.IsNotNull(user.Consents);
            Assert.AreEqual(1, user.Consents.Count);
            Assert.AreEqual(EnumConsent.email_notifications, user.Consents.First().Id);
            Assert.False(user.Consents.First().Enabled);
        }
    }
}
