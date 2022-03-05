namespace PreferenceCenterAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consent",
                c => new
                    {
                        Key = c.Long(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.UserPreference", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPreference",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consent", "UserId", "dbo.UserPreference");
            DropIndex("dbo.Consent", new[] { "UserId" });
            DropTable("dbo.UserPreference");
            DropTable("dbo.Consent");
        }
    }
}
