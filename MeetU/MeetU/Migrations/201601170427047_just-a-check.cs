namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justacheck : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Tags", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.Tags", "Name", c => c.String(maxLength: 20));
        }
    }
}
