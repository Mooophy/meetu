namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removesomeid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "SomeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SomeID", c => c.Int(nullable: false));
        }
    }
}
