namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportedByForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "ReportedBy_StakeHolderId", "dbo.StakeHolders");
            DropIndex("dbo.Jobs", new[] { "ReportedBy_StakeHolderId" });
            RenameColumn(table: "dbo.Jobs", name: "ReportedBy_StakeHolderId", newName: "ReportedById");
            AlterColumn("dbo.Jobs", "ReportedById", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "ReportedById");
            AddForeignKey("dbo.Jobs", "ReportedById", "dbo.StakeHolders", "StakeHolderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ReportedById", "dbo.StakeHolders");
            DropIndex("dbo.Jobs", new[] { "ReportedById" });
            AlterColumn("dbo.Jobs", "ReportedById", c => c.Int());
            RenameColumn(table: "dbo.Jobs", name: "ReportedById", newName: "ReportedBy_StakeHolderId");
            CreateIndex("dbo.Jobs", "ReportedBy_StakeHolderId");
            AddForeignKey("dbo.Jobs", "ReportedBy_StakeHolderId", "dbo.StakeHolders", "StakeHolderId");
        }
    }
}
