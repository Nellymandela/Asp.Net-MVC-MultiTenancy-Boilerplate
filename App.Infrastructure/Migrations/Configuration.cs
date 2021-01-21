namespace App.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<App.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(App.Infrastructure.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            new SeedData.SeedApplicationPermissions(context).Seed();
            var tenants = new SeedData.SeedDefaultTenants(context, Core.Common.TenantStore.ApplicationDefaultTenants).Seed();
            new SeedData.SeedTenantsRolesAndUsers(context, tenants).Seed();
        }
    }
}
