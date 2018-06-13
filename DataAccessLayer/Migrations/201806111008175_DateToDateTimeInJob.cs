namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateToDateTimeInJob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Jobs", "UpdatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "UpdatedOn", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Jobs", "CreatedOn", c => c.DateTime(storeType: "date"));
        }
    }
}
