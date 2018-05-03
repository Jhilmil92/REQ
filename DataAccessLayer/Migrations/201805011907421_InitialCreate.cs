namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobDescription = c.String(),
                        JobType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        EstimatedTime = c.DateTime(nullable: false),
                        ActualTimeTaken = c.DateTime(nullable: false),
                        JobPriority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        AssignedTo_TakerId = c.Int(),
                        ReportedBy_StakeHolderId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Takers", t => t.AssignedTo_TakerId)
                .ForeignKey("dbo.StakeHolders", t => t.ReportedBy_StakeHolderId)
                .Index(t => t.AssignedTo_TakerId)
                .Index(t => t.ReportedBy_StakeHolderId);
            
            CreateTable(
                "dbo.Takers",
                c => new
                    {
                        TakerId = c.Int(nullable: false, identity: true),
                        TakerName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.TakerId);
            
            CreateTable(
                "dbo.StakeHolders",
                c => new
                    {
                        StakeHolderId = c.Int(nullable: false, identity: true),
                        StakeHolderOrganization = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.StakeHolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ReportedBy_StakeHolderId", "dbo.StakeHolders");
            DropForeignKey("dbo.Jobs", "AssignedTo_TakerId", "dbo.Takers");
            DropIndex("dbo.Jobs", new[] { "ReportedBy_StakeHolderId" });
            DropIndex("dbo.Jobs", new[] { "AssignedTo_TakerId" });
            DropTable("dbo.StakeHolders");
            DropTable("dbo.Takers");
            DropTable("dbo.Jobs");
        }
    }
}
