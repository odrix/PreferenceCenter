using System.ComponentModel.DataAnnotations;

namespace PreferenceCenterAPI.Domain
{
    public class Consent
    {
        [Key]
        public long Key { get; set; }

        [EnumDataType(typeof(EnumConsent))]
        public  EnumConsent Id { get; set; }

        public bool Enabled { get; set; }
    }
}