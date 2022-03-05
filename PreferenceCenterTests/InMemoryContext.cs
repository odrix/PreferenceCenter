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
        public void AddUser(UserPreference newUser) => _users.Add(newUser);

        public int DeleteUser(Guid id) => _users.RemoveAll(u => u.Id == id);

        public int DeleteUser(string email) => _users.RemoveAll(u =>u.Email == email);

        public UserPreference GetUser(Guid id) => _users.SingleOrDefault(u => u.Id == id);

        public UserPreference GetUser(string email) => _users.SingleOrDefault(u => u.Email == email);
    }
}
