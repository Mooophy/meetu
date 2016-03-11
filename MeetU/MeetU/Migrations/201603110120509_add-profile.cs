namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprofile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GoogleId = c.String(),
                        NickName = c.String(),
                        GivenName = c.String(),
                        FamilyName = c.String(),
                        Pricture = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropTable("dbo.Profiles");
        }
    }
}
