namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastcommit : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Meetups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Topics", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TopicMeetups", "Topic_Id", "dbo.Topics");
            //DropForeignKey("dbo.TopicMeetups", "Meetup_Id", "dbo.Meetups");
            //DropForeignKey("dbo.AspNetUsers", "Meetup_Id", "dbo.Meetups");
            DropForeignKey("dbo.Meetups", "Sponsor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Meetup_Id1", "dbo.Meetups");
            DropIndex("dbo.Meetups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Meetups", new[] { "Sponsor_Id" });
            //DropIndex("dbo.AspNetUsers", new[] { "Meetup_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Meetup_Id1" });
            DropIndex("dbo.Topics", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.TopicMeetups", new[] { "Topic_Id" });
            DropIndex("dbo.TopicMeetups", new[] { "Meetup_Id" });
            //DropColumn("dbo.AspNetUsers", "Meetup_Id");
            //DropColumn("dbo.AspNetUsers", "Meetup_Id1");
            DropTable("dbo.Meetups");
            DropTable("dbo.Topics");
            DropTable("dbo.TopicMeetups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TopicMeetups",
                c => new
                    {
                        Topic_Id = c.Int(nullable: false),
                        Meetup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Topic_Id, t.Meetup_Id });
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedByUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meetups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        AddressPlanned = c.String(),
                        TimePLanned = c.DateTime(nullable: false),
                        Decided = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Sponsor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Meetup_Id1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Meetup_Id", c => c.Int());
            CreateIndex("dbo.TopicMeetups", "Meetup_Id");
            CreateIndex("dbo.TopicMeetups", "Topic_Id");
            CreateIndex("dbo.Topics", "CreatedByUser_Id");
            CreateIndex("dbo.AspNetUsers", "Meetup_Id1");
            CreateIndex("dbo.AspNetUsers", "Meetup_Id");
            CreateIndex("dbo.Meetups", "Sponsor_Id");
            CreateIndex("dbo.Meetups", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Meetup_Id1", "dbo.Meetups", "Id");
            AddForeignKey("dbo.Meetups", "Sponsor_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Meetup_Id", "dbo.Meetups", "Id");
            AddForeignKey("dbo.TopicMeetups", "Meetup_Id", "dbo.Meetups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TopicMeetups", "Topic_Id", "dbo.Topics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Topics", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Meetups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
