namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estimatedandactualtime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "EstimatedTime", c => c.String());
            AlterColumn("dbo.Jobs", "ActualTimeTaken", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "ActualTimeTaken", c => c.DateTime());
            AlterColumn("dbo.Jobs", "EstimatedTime", c => c.DateTime());
        }
    }
}
