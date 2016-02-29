namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removewatch : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Watches", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Watches", "MeetupId", "dbo.Meetups");
            DropIndex("dbo.Watches", new[] { "MeetupId" });
            DropIndex("dbo.Watches", new[] { "UserId" });
            DropTable("dbo.Watches");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Watches",
                c => new
                    {
                        MeetupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.MeetupId, t.UserId });
            
            CreateIndex("dbo.Watches", "UserId");
            CreateIndex("dbo.Watches", "MeetupId");
            AddForeignKey("dbo.Watches", "MeetupId", "dbo.Meetups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Watches", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
