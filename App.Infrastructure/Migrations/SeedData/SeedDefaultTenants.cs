using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Migrations.SeedData
{
    public class SeedDefaultTenants : DataSeedBase
    {
        private readonly List<Core.Models.Entities.TenantViewModel> _tenants;
        public SeedDefaultTenants(ApplicationDbContext context, List<Core.Models.Entities.TenantViewModel> tenants)
        {
            _context = context;
            _tenants = tenants;
        }

        public List<App.Core.Domain.Entities.Tenant> Seed()
        {
            List<App.Core.Domain.Entities.Tenant> tenants = new List<Core.Domain.Entities.Tenant>();
            foreach (Core.Models.Entities.TenantViewModel tenantVM in _tenants)
            {
                tenants.Add(Seed(tenantVM));
            }
            return tenants;
        }

        private App.Core.Domain.Entities.Tenant Seed(Core.Models.Entities.TenantViewModel tenantVM)
        {
            if (!_context.Tenants.Any(r => r.Code.Equals(tenantVM.Code)))
            {
                App.Core.Domain.Entities.Tenant tenant = new App.Core.Domain.Entities.Tenant()
                {
                    Name = tenantVM.Name,
                    Code = tenantVM.Code,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                };
                _context.Tenants.Add(tenant);
                _context.SaveChanges();
                return tenant;
            }
            else
            {
                return _context.Tenants.FirstOrDefault(r => r.Code.Equals(tenantVM.Code));
            }
        }
    }
}
