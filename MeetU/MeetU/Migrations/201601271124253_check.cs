namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Author", c => c.String());
            AddColumn("dbo.Comments", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Discriminator");
            DropColumn("dbo.Comments", "Author");
        }
    }
}
