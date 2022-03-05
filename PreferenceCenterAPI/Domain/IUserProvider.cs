
namespace PreferenceCenterAPI.Domain
{
    public interface IUserProvider
    {
        void AddUser(UserPreference newUser);
        int DeleteUser(Guid id);
        int DeleteUser(string email);
        UserPreference GetUser(Guid id);
        UserPreference GetUser(string email);

        bool GetLastConsentOf(Guid userId, EnumConsent consent);
    }
}