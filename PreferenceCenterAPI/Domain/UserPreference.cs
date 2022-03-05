namespace PreferenceCenterAPI.Domain
{
    public class UserPreference
    {
        public UserPreference()
        {
            Consents = new List<Consent>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<Consent> Consents { get; set; }
    }
}