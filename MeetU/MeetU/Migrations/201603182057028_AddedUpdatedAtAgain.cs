namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUpdatedAtAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "UpdatedAt");
        }
    }
}
