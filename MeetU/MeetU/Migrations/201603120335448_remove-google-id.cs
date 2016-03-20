namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removegoogleid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "GoogleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "GoogleId", c => c.String());
        }
    }
}
