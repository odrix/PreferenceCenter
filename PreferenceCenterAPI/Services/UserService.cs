using PreferenceCenterAPI.Models;
using System.Text.RegularExpressions;

namespace PreferenceCenterAPI.Services
{
    internal class UserService : IUserService
    {
        List<UserPreference> _users;
        public UserService()
        {
            _users = new List<UserPreference>();
        }

        public UserPreference Get(Guid id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public UserPreference Get(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }

        public UserPreference Add(UserPreference newUser)
        {
            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));   

            if(!IsEmailValid(newUser.Email))
                throw new ArgumentException("Email already exist.", nameof(newUser));


            if (Get(newUser.Id) == null)
                throw new ArgumentException("Id already exist.", nameof(newUser));

            if (Get(newUser.Email) == null)
                throw new ArgumentException("Email already exist.", nameof(newUser));

            if (newUser.Id == Guid.Empty)
                newUser.Id = Guid.NewGuid();

            _users.Add(newUser);

            return newUser;
        }

        private bool IsEmailValid(string email)
        {
            var emailValidator = new Regex("");
            return emailValidator.IsMatch(email);
        }

        public void Delete(Guid id)
        {
            _users.RemoveAll(x => x.Id == id);
        }

        public void Delete(string email)
        {
            _users.RemoveAll(x => x.Email == email);
        }
    }
}