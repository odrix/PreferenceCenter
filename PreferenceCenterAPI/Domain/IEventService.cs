using PreferenceCenterAPI.Domain;

namespace PreferenceCenterAPI.Domain
{
    public interface IEventService
    {
        void AddEvents(Guid userId, Consent[] consents);
    }
}