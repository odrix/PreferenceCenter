namespace PreferenceCenterAPI.Domain
{
    public interface IEventProvider
    {
        int AddEvents(Guid userId, Consent[] consents);
        bool CheckUserExist(Guid id);
    }
}