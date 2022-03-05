using PreferenceCenterAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenceCenterTests
{
    internal class InMemoryContext : IUserProvider
    {
        List<UserPreference> _users = new List<UserPreference>();
        public void Add(UserPreference newUser) => _users.Add(newUser);

        public int Delete(Guid id) => _users.RemoveAll(u => u.Id == id);

        public int Delete(string email) => _users.RemoveAll(u =>u.Email == email);

        public UserPreference Get(Guid id) => _users.SingleOrDefault(u => u.Id == id);

        public UserPreference Get(string email) => _users.SingleOrDefault(u => u.Email == email);
    }
}
