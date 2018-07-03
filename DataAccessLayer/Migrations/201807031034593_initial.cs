namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientOrganization = c.String(),
                        JoinDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobTitle = c.String(),
                        JobId = c.Int(nullable: false, identity: true),
                        JobDescription = c.String(),
                        JobCategory = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        ReportedById = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        EstimatedTime = c.String(),
                        ActualTimeTaken = c.String(),
                        AssignedToId = c.Int(),
                        JobPriority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ReleaseVersion = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Takers", t => t.AssignedToId)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ReportedById, cascadeDelete: true)
                .Index(t => t.ReportedById)
                .Index(t => t.CreatedById)
                .Index(t => t.AssignedToId);
            
            CreateTable(
                "dbo.Takers",
                c => new
                    {
                        TakerId = c.Int(nullable: false, identity: true),
                        TakerName = c.String(),
                    })
                .PrimaryKey(t => t.TakerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100, unicode: false),
                        Password = c.String(),
                        UserType = c.Int(nullable: false),
                        TargetUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        StaffName = c.String(),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.StakeHolders",
                c => new
                    {
                        StakeHolderId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StakeHolderId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StakeHolders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Jobs", "ReportedById", "dbo.Clients");
            DropForeignKey("dbo.Jobs", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Jobs", "AssignedToId", "dbo.Takers");
            DropIndex("dbo.StakeHolders", new[] { "ClientId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.Jobs", new[] { "AssignedToId" });
            DropIndex("dbo.Jobs", new[] { "CreatedById" });
            DropIndex("dbo.Jobs", new[] { "ReportedById" });
            DropTable("dbo.StakeHolders");
            DropTable("dbo.Staffs");
            DropTable("dbo.Users");
            DropTable("dbo.Takers");
            DropTable("dbo.Jobs");
            DropTable("dbo.Clients");
        }
    }
}
