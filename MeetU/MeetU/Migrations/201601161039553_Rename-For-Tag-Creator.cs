namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameForTagCreator : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tags", name: "CreatedByUser", newName: "By");
            RenameIndex(table: "dbo.Tags", name: "IX_CreatedByUser", newName: "IX_By");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tags", name: "IX_By", newName: "IX_CreatedByUser");
            RenameColumn(table: "dbo.Tags", name: "By", newName: "CreatedByUser");
        }
    }
}
