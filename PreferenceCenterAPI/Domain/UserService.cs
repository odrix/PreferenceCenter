using System.Text.RegularExpressions;

namespace PreferenceCenterAPI.Domain
{
    public class UserService : IUserService
    {
        IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public UserPreference Get(Guid id) => _userProvider.Get(id);

        public UserPreference Get(string email) => _userProvider.Get(email);    

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

            _userProvider.Add(newUser);

            return newUser;
        }

        private bool IsEmailValid(string email)
        {
            var emailValidator = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return emailValidator.IsMatch(email);
        }

        public bool Delete(Guid id)
        {
            return _userProvider.Delete(id) == 1;
        }

        public bool Delete(string email)
        {
           return _userProvider.Delete(email) == 1;
        }
    }
}