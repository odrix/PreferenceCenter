using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PreferenceCenterAPI.Domain
{
    public class Consent
    {
        [EnumDataType(typeof(EnumConsent))]
        public EnumConsent Id { get; set; }

        public bool Enabled { get; set; }

    }
}