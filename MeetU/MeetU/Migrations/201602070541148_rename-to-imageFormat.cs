namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renametoimageFormat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileImages", "ImageFormat", c => c.String(nullable: false));
            DropColumn("dbo.ProfileImages", "ImageType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProfileImages", "ImageType", c => c.String(nullable: false));
            DropColumn("dbo.ProfileImages", "ImageFormat");
        }
    }
}
