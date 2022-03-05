namespace PreferenceCenterAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PreferenceCenterAPI.DAL.PreferenceCenterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PreferenceCenterAPI.DAL.PreferenceCenterContext";
        }

        protected override void Seed(PreferenceCenterAPI.DAL.PreferenceCenterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
