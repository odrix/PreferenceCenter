namespace PreferenceCenterAPI.Domain
{
    public class EventService : IEventService
    {
        private readonly IEventProvider _eventProvider;

        public EventService(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        public void AddEvents(Guid userId, Consent[] consents)
        {
            if(consents == null || consents.Length == 0)
                throw new ArgumentException(nameof(consents));

            if(!_eventProvider.CheckUserExist(userId))
                throw new ArgumentException(nameof(userId));

            _eventProvider.AddEvents(userId, consents);
        }
    }
}
