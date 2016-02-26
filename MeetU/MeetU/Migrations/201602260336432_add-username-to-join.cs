namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusernametojoin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Joins", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Joins", "UserName");
        }
    }
}
