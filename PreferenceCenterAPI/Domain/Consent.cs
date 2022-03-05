using System.ComponentModel.DataAnnotations.Schema;

namespace PreferenceCenterAPI.Domain
{
    public class Consent
    {
        public long Id { get; set; }
        public  string Name { get; set; }
        public bool Enabled { get; set; }
    }
}