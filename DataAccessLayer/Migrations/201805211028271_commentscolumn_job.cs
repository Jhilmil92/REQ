namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentscolumn_job : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Comments");
        }
    }
}
