namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCancelledAtNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meetups", "CancelledAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meetups", "CancelledAt", c => c.DateTime(nullable: false));
        }
    }
}
