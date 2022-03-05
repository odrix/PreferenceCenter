using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreferenceCenterAPI.Domain
{
    public class Consent : IComparable<Consent>, IComparable
    {
        [Key]
        public long Key { get; set; }

        
        [EnumDataType(typeof(EnumConsent))]
        public  EnumConsent Id { get; set; }

        public bool Enabled { get; set; }
        
        public Guid UserId { get; set; }

        

        public int CompareTo(object? obj) => Key.CompareTo(obj);

        public int CompareTo(Consent? other) => Key.CompareTo(other);
    }
}