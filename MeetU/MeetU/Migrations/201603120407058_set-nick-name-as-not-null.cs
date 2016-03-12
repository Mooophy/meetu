namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setnicknameasnotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profiles", "NickName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profiles", "NickName", c => c.String());
        }
    }
}
