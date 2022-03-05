
namespace PreferenceCenterAPI.Domain
{
    public interface IUserService
    {
        UserPreference Add(string email);
        bool Delete(Guid id);
        bool Delete(string email);
        UserPreference Get(Guid id);
        UserPreference Get(string email);
    }
}