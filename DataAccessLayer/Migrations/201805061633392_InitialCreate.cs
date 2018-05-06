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
                        JobCategory = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ReportedById = c.Int(nullable: false),
                        EstimatedTime = c.DateTime(),
                        ActualTimeTaken = c.DateTime(),
                        AssignedToId = c.Int(),
                        JobPriority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Takers", t => t.AssignedToId)
                .ForeignKey("dbo.StakeHolders", t => t.ReportedById, cascadeDelete: true)
                .Index(t => t.ReportedById)
                .Index(t => t.AssignedToId);
            
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
            DropForeignKey("dbo.Jobs", "ReportedById", "dbo.StakeHolders");
            DropForeignKey("dbo.Jobs", "AssignedToId", "dbo.Takers");
            DropIndex("dbo.Jobs", new[] { "AssignedToId" });
            DropIndex("dbo.Jobs", new[] { "ReportedById" });
            DropTable("dbo.StakeHolders");
            DropTable("dbo.Takers");
            DropTable("dbo.Jobs");
        }
    }
}
