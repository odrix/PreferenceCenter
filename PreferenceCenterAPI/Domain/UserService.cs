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
            if (user == null)
                return null;

            KeepOnlyLastEvents(user);
            return user;
        }

        public UserPreference Get(string email)
        {
            var user = _userProvider.GetUser(email);
            if (user == null)
                return null;

            KeepOnlyLastEvents(user);
            return user;
        }

        public UserPreference Add(string email)
        {
            if (!IsEmailValid(email))
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

        public bool Delete(Guid id)
        {
            return _userProvider.DeleteUser(id) > 0;
        }

        public bool Delete(string email)
        {
            return _userProvider.DeleteUser(email) > 0;
        }

        private bool IsEmailValid(string email)
        {
            var emailValidator = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return emailValidator.IsMatch(email);
        }

        private void KeepOnlyLastEvents(UserPreference user)
        {
            var emailConsent = _userProvider.GetLastConsentOf(user.Id, EnumConsent.email_notifications);
            user.Consents.Add(new Consent() { Id = EnumConsent.email_notifications, Enabled = emailConsent});

            var smsConsent = _userProvider.GetLastConsentOf(user.Id, EnumConsent.sms_notifications);
            user.Consents.Add(new Consent() { Id = EnumConsent.sms_notifications, Enabled = smsConsent });
        }
    }
}