namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedByColumnAlteration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "CreatedOn", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "CreatedOn", c => c.DateTime());
        }
    }
}
