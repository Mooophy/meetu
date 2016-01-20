namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Join : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Joins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Meetups", t => t.MeetupId, cascadeDelete: true)
                .Index(t => t.MeetupId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Joins", "MeetupId", "dbo.Meetups");
            DropForeignKey("dbo.Joins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Joins", new[] { "UserId" });
            DropIndex("dbo.Joins", new[] { "MeetupId" });
            DropTable("dbo.Joins");
        }
    }
}
