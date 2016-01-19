namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertagain : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TestDateTimes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestDateTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        When = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
