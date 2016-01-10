namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SomeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SomeID");
        }
    }
}
