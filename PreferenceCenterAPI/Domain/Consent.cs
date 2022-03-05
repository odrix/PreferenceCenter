using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PreferenceCenterAPI.Domain
{
    public class Consent
    {
        [Key]
        [JsonIgnore]
        public long Key { get; set; }


        [EnumDataType(typeof(EnumConsent))]
        public EnumConsent Id { get; set; }

        public bool Enabled { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

    }
}