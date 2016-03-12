namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginCountCanntBeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profiles", "LoginCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profiles", "LoginCount", c => c.Int());
        }
    }
}
