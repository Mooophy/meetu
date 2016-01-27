namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 200),
                        By = c.String(maxLength: 128),
                        MeetupId = c.Int(nullable: false),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.By)
                .ForeignKey("dbo.Meetups", t => t.MeetupId, cascadeDelete: true)
                .Index(t => t.By)
                .Index(t => t.MeetupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "MeetupId", "dbo.Meetups");
            DropForeignKey("dbo.Comments", "By", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "MeetupId" });
            DropIndex("dbo.Comments", new[] { "By" });
            DropTable("dbo.Comments");
        }
    }
}
