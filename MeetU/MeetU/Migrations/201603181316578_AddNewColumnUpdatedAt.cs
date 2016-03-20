namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnUpdatedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "UpdatedAt", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "UpdatedAt");
        }
    }
}
