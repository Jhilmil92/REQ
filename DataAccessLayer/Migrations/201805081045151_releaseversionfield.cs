namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class releaseversionfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ReleaseVersion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "ReleaseVersion");
        }
    }
}
