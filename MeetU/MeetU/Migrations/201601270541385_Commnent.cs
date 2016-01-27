namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Commnent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 200),
                        By = c.String(maxLength: 128),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.By)
                .Index(t => t.By);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "By", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "By" });
            DropTable("dbo.Comments");
        }
    }
}
