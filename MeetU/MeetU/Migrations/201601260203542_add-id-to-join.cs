namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidtojoin : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Joins");
            AddColumn("dbo.Joins", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Joins", new[] { "MeetupId", "UserId", "Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Joins");
            DropColumn("dbo.Joins", "Id");
            AddPrimaryKey("dbo.Joins", new[] { "MeetupId", "UserId" });
        }
    }
}
