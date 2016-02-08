namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProfileDataUri : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileDataUris",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Image = c.String(),
                        ImageFormat = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileDataUris", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileDataUris", new[] { "UserId" });
            DropTable("dbo.ProfileDataUris");
        }
    }
}
