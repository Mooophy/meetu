namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileImages", "ImageType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileImages", "ImageType");
        }
    }
}
