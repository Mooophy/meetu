namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "By", "dbo.AspNetUsers");
            DropIndex("dbo.Tags", new[] { "By" });
            DropTable("dbo.Tags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 100),
                        By = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Tags", "By");
            AddForeignKey("dbo.Tags", "By", "dbo.AspNetUsers", "Id");
        }
    }
}
