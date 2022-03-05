namespace PreferenceCenterAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Consent", "UserId", "dbo.UserPreference");
            DropIndex("dbo.Consent", new[] { "UserId" });
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Created = c.DateTime(nullable: false),
                        Id = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Created)
                .Index(t => t.Id);
            
            DropTable("dbo.Consent");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Key);
            
            DropIndex("dbo.Event", new[] { "Id" });
            DropTable("dbo.Event");
            CreateIndex("dbo.Consent", "UserId");
            AddForeignKey("dbo.Consent", "UserId", "dbo.UserPreference", "Id", cascadeDelete: true);
        }
    }
}
