namespace PreferenceCenterAPI.Domain
{
    public class EventService : IEventService
    {
        private IEventProvider _eventProvider;
        public EventService(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        public void AddEvents(Guid userId, Consent[] consents)
        {
            if(!_eventProvider.CheckUserExist(userId))
                throw new Exception();

            foreach (var consent in consents)
                consent.UserId = userId;
            _eventProvider.AddEvents(consents);
        }
    }
}
