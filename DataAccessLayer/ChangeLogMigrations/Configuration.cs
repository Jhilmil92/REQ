namespace DataAccessLayer.ChangeLogMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Req.Data.Infrastructure.Classes.ChangeLogDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"ChangeLogMigrations";
            ContextKey = "DataAccessLayer.Req.Data.Infrastructure.Classes.ChangeLogDataContext";
        }

        protected override void Seed(DataAccessLayer.Req.Data.Infrastructure.Classes.ChangeLogDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
