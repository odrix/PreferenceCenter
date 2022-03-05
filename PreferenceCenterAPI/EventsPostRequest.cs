using PreferenceCenterAPI.Domain;

namespace PreferenceCenterAPI
{
    public class EventsPostRequest
    {
        public UserId User { get; set; }
        public Consent[] Consents { get; set; }
    }

    public class UserId
    {
        public Guid Id { get; set; }
    }
}
