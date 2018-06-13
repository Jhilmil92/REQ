namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastUpdatedOnColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "UpdatedOn", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "UpdatedOn");
        }
    }
}
