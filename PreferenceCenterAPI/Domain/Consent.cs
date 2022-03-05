using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreferenceCenterAPI.Domain
{
    public class Consent
    {
        [Key]
        public long Key { get; set; }


        [EnumDataType(typeof(EnumConsent))]
        public EnumConsent Id { get; set; }

        public bool Enabled { get; set; }

        public Guid UserId { get; set; }

    }
}