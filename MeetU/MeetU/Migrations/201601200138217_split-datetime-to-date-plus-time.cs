namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class splitdatetimetodateplustime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetups", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Meetups", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Meetups", "When", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meetups", "When", c => c.DateTime(nullable: false));
            DropColumn("dbo.Meetups", "CreatedAt");
            DropColumn("dbo.Meetups", "Date");
        }
    }
}
