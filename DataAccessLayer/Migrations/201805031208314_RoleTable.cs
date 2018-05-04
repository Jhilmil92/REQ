namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            AddColumn("dbo.Takers", "TakerRoleId", c => c.Int());
            AddColumn("dbo.StakeHolders", "StakeHolderRoleId", c => c.Int());
            CreateIndex("dbo.Takers", "TakerRoleId");
            CreateIndex("dbo.StakeHolders", "StakeHolderRoleId");
            AddForeignKey("dbo.Takers", "TakerRoleId", "dbo.Roles", "RoleId");
            AddForeignKey("dbo.StakeHolders", "StakeHolderRoleId", "dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StakeHolders", "StakeHolderRoleId", "dbo.Roles");
            DropForeignKey("dbo.Takers", "TakerRoleId", "dbo.Roles");
            DropIndex("dbo.StakeHolders", new[] { "StakeHolderRoleId" });
            DropIndex("dbo.Takers", new[] { "TakerRoleId" });
            DropColumn("dbo.StakeHolders", "StakeHolderRoleId");
            DropColumn("dbo.Takers", "TakerRoleId");
            DropTable("dbo.Roles");
        }
    }
}
