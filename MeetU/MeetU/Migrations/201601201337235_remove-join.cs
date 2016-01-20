namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removejoin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Joins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Joins", "MeetupId", "dbo.Meetups");
            DropIndex("dbo.Joins", new[] { "MeetupId" });
            DropIndex("dbo.Joins", new[] { "UserId" });
            DropTable("dbo.Joins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Joins",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MeetupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.MeetupId, t.UserId });
            
            CreateIndex("dbo.Joins", "UserId");
            CreateIndex("dbo.Joins", "MeetupId");
            AddForeignKey("dbo.Joins", "MeetupId", "dbo.Meetups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Joins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
