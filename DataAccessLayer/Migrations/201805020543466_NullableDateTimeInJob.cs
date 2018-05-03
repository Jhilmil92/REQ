namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDateTimeInJob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Jobs", "EstimatedTime", c => c.DateTime());
            AlterColumn("dbo.Jobs", "ActualTimeTaken", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "ActualTimeTaken", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "EstimatedTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
