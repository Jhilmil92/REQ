namespace DataAccessLayer.ChangeLogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedByField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeLogs", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeLogs", "CreatedBy");
        }
    }
}
