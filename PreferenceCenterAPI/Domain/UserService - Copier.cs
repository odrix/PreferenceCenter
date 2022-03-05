using System.Text.RegularExpressions;

namespace PreferenceCenterAPI.Domain
{
    public class UserService : IUserService
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

        public UserPreference Add(string email)
        {
            if(!IsEmailValid(email))
                throw new FormatException("Email is wrong formated.");

            if (Get(email) != null)
                throw new ArgumentException("email already exist.", nameof(email));

            var newUser = new UserPreference()
            {
                Id = Guid.NewGuid(),
                Email = email,
            };

            _users.Add(newUser);

            return newUser;
        }

        private bool IsEmailValid(string email)
        {
            var emailValidator = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return emailValidator.IsMatch(email);
        }

        public bool Delete(Guid id)
        {
            return _users.RemoveAll(x => x.Id == id) == 1;
        }

        public bool Delete(string email)
        {
           return _users.RemoveAll(x => x.Email == email) == 1;
        }
    }
}