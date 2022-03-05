using System.Text.RegularExpressions;

namespace PreferenceCenterAPI.Domain
{
    public class UserService : IUserService
    {
        readonly IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public UserPreference Get(Guid id)
        {
            var user = _userProvider.GetUser(id);
            if(user == null)
                return null;
            user.Consents.Sort(new ConsentKeyComparer(desc: true));
            return user;
        }

        public UserPreference Get(string email)
        {
            var user = _userProvider.GetUser(email);
            if (user == null)
                return null;
            user.Consents.Sort(new ConsentKeyComparer(desc: true));
            return user;
        }

        public UserPreference Add(string email)
        {
            if(!IsEmailValid(email))
                throw new ArgumentException("Email is wrong formated.", nameof(email));

            if (Get(email) != null)
                throw new ArgumentException("email already exist.", nameof(email));

            var newUser = new UserPreference()
            {
                Id = Guid.NewGuid(),
                Email = email,
            };

            _userProvider.AddUser(newUser);

            return newUser;
        }

        private bool IsEmailValid(string email)
        {
            var emailValidator = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return emailValidator.IsMatch(email);
        }

        public bool Delete(Guid id)
        {
            return _userProvider.DeleteUser(id) == 1;
        }

        public bool Delete(string email)
        {
           return _userProvider.DeleteUser(email) == 1;
        }
    }
}