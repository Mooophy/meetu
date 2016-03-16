namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCancelFeatrueForMeetups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetups", "IsCancelled", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Meetups", "CancelledAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetups", "CancelledAt");
            DropColumn("dbo.Meetups", "IsCancelled");
        }
    }
}
