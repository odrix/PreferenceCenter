
namespace PreferenceCenterAPI.Domain
{
    public interface IUserProvider
    {
        void Add(UserPreference newUser);
        int Delete(Guid id);
        int Delete(string email);
        UserPreference Get(Guid id);
        UserPreference Get(string email);
    }
}