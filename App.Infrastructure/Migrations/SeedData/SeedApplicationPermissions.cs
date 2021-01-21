using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Migrations.SeedData
{
    public class SeedApplicationPermissions : DataSeedBase
    {
        public SeedApplicationPermissions(ApplicationDbContext context)
        {
            _context = context;
        }

    public void Seed()
        {
            foreach (var permission in Core.Models.MenuFactory.GetMenus(new List<App.Core.Models.MenuViewModel>(), Core.Common.ApplicationStore.ApplicationMenu))
            {
                if (permission.IsPermission == true && !_context.Permissions.Any(u => u.Type.Equals(permission.Claim.Type)))
                {
                    _context.Permissions.Add(new App.Core.Domain.Entities.Permission()
                    {
                        Type = permission.Claim.Type,
                        Value = permission.Name,
                        IsActive = true,
                        IsDeleted = false,
                        DateCreated = DateTime.Now
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
