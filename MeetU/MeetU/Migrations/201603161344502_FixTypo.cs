namespace MeetU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "Picture", c => c.String());
            DropColumn("dbo.Profiles", "Pricture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "Pricture", c => c.String());
            DropColumn("dbo.Profiles", "Picture");
        }
    }
}
