using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreferenceCenterAPI.Domain
{
    public class UserPreference
    {
        public UserPreference()
        {
            Consents = new List<Consent>();
        }

        public Guid Id { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [NotMapped]
        public List<Consent> Consents { get; set; }
    }
}