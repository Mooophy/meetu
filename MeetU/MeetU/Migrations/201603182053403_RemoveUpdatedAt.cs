namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUpdatedAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
