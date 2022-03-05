using NUnit.Framework.Internal.Execution;
using PreferenceCenterAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenceCenterTests
{
    internal class InMemoryContext : IUserProvider, IEventProvider
    {
        List<UserPreference> _users = new List<UserPreference>();
        List<PreferenceCenterAPI.DAL.Event> _events = new List<PreferenceCenterAPI.DAL.Event>();

        public int AddEvents(Guid userId, Consent[] events)
        {
            _events.AddRange(events.Select(c => new PreferenceCenterAPI.DAL.Event()
            {
                Created = DateTime.Now,
                Enabled = c.Enabled,
                Id = c.Id,
                UserId = userId,
            }));
            return events.Length;
        }

        public void AddUser(UserPreference newUser) => _users.Add(newUser);

        public bool CheckUserExist(Guid id) => _users.Any(u => u.Id == id);

        public int DeleteUser(Guid id) => _users.RemoveAll(u => u.Id == id);

        public int DeleteUser(string email) => _users.RemoveAll(u =>u.Email == email);

        public bool GetLastConsentOf(Guid userId, EnumConsent consent)
        {
            return (from x in _events
                    where x.UserId == userId && x.Id == consent
                    orderby x.Created descending
                    select x.Enabled).FirstOrDefault();
        }

        public UserPreference GetUser(Guid id) => _users.SingleOrDefault(u => u.Id == id);

        public UserPreference GetUser(string email) => _users.SingleOrDefault(u => u.Email == email);
    }
}
