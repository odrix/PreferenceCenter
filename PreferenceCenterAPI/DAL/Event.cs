using PreferenceCenterAPI.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreferenceCenterAPI.DAL
{
    public class Event
    {
        [Key]
        public DateTime Created { get; set; }

        [Index]
        [EnumDataType(typeof(EnumConsent))]
        public EnumConsent Id { get; set; }

        public bool Enabled { get; set; }

        public Guid UserId { get; set; }
    }
}
