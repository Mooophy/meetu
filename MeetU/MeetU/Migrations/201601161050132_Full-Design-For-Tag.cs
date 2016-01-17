namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullDesignForTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "Name", c => c.String(maxLength: 20));
            AddColumn("dbo.Tags", "Description", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "Description");
            DropColumn("dbo.Tags", "Name");
        }
    }
}
