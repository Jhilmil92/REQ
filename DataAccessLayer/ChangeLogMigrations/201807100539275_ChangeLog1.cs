namespace DataAccessLayer.ChangeLogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLog1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeLogs", "LoggedBy", c => c.String());
            DropColumn("dbo.ChangeLogs", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChangeLogs", "CreatedBy", c => c.String());
            DropColumn("dbo.ChangeLogs", "LoggedBy");
        }
    }
}
