namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompisitkeytoJoin : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Joins");
            AddPrimaryKey("dbo.Joins", new[] { "MeetupId", "UserId" });
            DropColumn("dbo.Joins", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Joins", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Joins");
            AddPrimaryKey("dbo.Joins", "Id");
        }
    }
}
