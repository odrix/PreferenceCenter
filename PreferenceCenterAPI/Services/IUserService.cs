using PreferenceCenterAPI.Models;

namespace PreferenceCenterAPI.Services
{
    public interface IUserService
    {
        UserPreference Add(string email);
        void Delete(Guid id);
        void Delete(string email);
        UserPreference Get(Guid id);
        UserPreference Get(string email);
    }
}