namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Follow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        FollowedUserId = c.String(nullable: false, maxLength: 128),
                        FollowingUserId = c.String(nullable: false, maxLength: 128),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FollowedUserId, t.FollowingUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowedUserId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowingUserId, cascadeDelete: false)
                .Index(t => t.FollowedUserId)
                .Index(t => t.FollowingUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "FollowingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Follows", "FollowedUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "FollowingUserId" });
            DropIndex("dbo.Follows", new[] { "FollowedUserId" });
            DropTable("dbo.Follows");
        }
    }
}
