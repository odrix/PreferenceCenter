using PreferenceCenterAPI.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PreferenceCenterAPI.DAL
{
    public class PreferenceCenterContext : DbContext
    {
        public PreferenceCenterContext() : base("PreferenceCenter")
        {
        }

        public DbSet<UserPreference> Users { get; set; }
        public DbSet<Consent> Consents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
