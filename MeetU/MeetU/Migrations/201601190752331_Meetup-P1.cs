namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetupP1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meetups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false),
                        When = c.DateTime(nullable: false),
                        Where = c.String(nullable: false),
                        Sponsor = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sponsor)
                .Index(t => t.Sponsor);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetups", "Sponsor", "dbo.AspNetUsers");
            DropIndex("dbo.Meetups", new[] { "Sponsor" });
            DropTable("dbo.Meetups");
        }
    }
}
