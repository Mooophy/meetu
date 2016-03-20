namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCreatedAtandLoginCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Profiles", "LoginCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "LoginCount");
            DropColumn("dbo.Profiles", "CreatedAt");
        }
    }
}
