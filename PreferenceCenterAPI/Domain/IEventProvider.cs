namespace PreferenceCenterAPI.Domain
{
    public interface IEventProvider
    {
        int AddEvents(Consent[] consents);
        bool CheckUserExist(Guid id);
    }
}