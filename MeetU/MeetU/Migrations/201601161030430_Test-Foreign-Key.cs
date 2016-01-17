namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "CreatedByUser", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tags", "CreatedByUser");
            AddForeignKey("dbo.Tags", "CreatedByUser", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "CreatedByUser", "dbo.AspNetUsers");
            DropIndex("dbo.Tags", new[] { "CreatedByUser" });
            DropColumn("dbo.Tags", "CreatedByUser");
        }
    }
}
