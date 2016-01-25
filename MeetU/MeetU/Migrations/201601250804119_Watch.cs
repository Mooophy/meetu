namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Watch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Watches",
                c => new
                    {
                        MeetupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.MeetupId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Meetups", t => t.MeetupId, cascadeDelete: true)
                .Index(t => t.MeetupId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Watches", "MeetupId", "dbo.Meetups");
            DropForeignKey("dbo.Watches", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Watches", new[] { "UserId" });
            DropIndex("dbo.Watches", new[] { "MeetupId" });
            DropTable("dbo.Watches");
        }
    }
}
