namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdentitySeperation : DbMigration
    {
        public override void Up()
        {
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
            
            DropColumn("dbo.Takers", "UserName");
            DropColumn("dbo.Takers", "Password");
            DropColumn("dbo.StakeHolders", "UserName");
            DropColumn("dbo.StakeHolders", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StakeHolders", "Password", c => c.String());
            AddColumn("dbo.StakeHolders", "UserName", c => c.String());
            AddColumn("dbo.Takers", "Password", c => c.String());
            AddColumn("dbo.Takers", "UserName", c => c.String());
            DropIndex("dbo.Users", new[] { "UserName" });
            DropTable("dbo.Users");
        }
    }
}
