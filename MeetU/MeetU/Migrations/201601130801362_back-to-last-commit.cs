namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backtolastcommit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meetups", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Meetups", new[] { "UserId" });
            DropTable("dbo.Meetups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Meetups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Meetups", "UserId");
            AddForeignKey("dbo.Meetups", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
