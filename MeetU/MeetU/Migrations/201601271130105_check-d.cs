namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Author");
            DropColumn("dbo.Comments", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Comments", "Author", c => c.String());
        }
    }
}
