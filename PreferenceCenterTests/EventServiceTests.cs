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
    public class EventServiceTests
    {
        [Test]
        public void AddManyEvents_ShouldAllHaveUserId()
        {
            Guid userId = Guid.NewGuid();
            Consent[] newConsents = new Consent[2] {
                new Consent { Id = EnumConsent.email_notifications, Enabled = true },
                new Consent { Id = EnumConsent.sms_notifications, Enabled = false },
            };
            var inMemoryContext = new InMemoryContext();
            inMemoryContext.AddUser(new UserPreference { Id = userId });

            EventService eventService = new EventService(inMemoryContext);
            eventService.AddEvents(userId, newConsents);

            var inMemoryUser = inMemoryContext.GetUser(userId);
            Assert.AreEqual(2, inMemoryUser.Consents.Count);
            Assert.That(inMemoryUser.Consents.All(x => x.UserId == userId));
        }

        [Test]
        public void AddEventToUnexistUser_ShouldThrowException()
        {
            Guid userId = Guid.NewGuid();
            Consent[] newConsents = new Consent[1] {
                new Consent { Id = EnumConsent.email_notifications, Enabled = true },
            };
            var inMemoryContext = new InMemoryContext();

            EventService eventService = new EventService(inMemoryContext);
            Assert.Throws<ArgumentException>(() => eventService.AddEvents(userId, newConsents));
        }

        [Test]
        public void AddNoEvents_ShouldThrowArgumentException()
        {
            Guid userId = Guid.NewGuid();
            Consent[] newConsents = new Consent[0];
            var inMemoryContext = new InMemoryContext();
            inMemoryContext.AddUser(new UserPreference { Id = userId });

            EventService eventService = new EventService(inMemoryContext);
            Assert.Throws<ArgumentException>(() => eventService.AddEvents(userId, newConsents));
        }
    }
}
