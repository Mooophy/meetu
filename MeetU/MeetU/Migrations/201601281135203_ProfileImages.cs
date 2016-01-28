namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileImages",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileImages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileImages", new[] { "UserId" });
            DropTable("dbo.ProfileImages");
        }
    }
}
