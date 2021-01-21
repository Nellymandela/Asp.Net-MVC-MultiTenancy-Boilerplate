using System;
using System.Collections.Generic;
using App.Core.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Common
{
    public class TenantStore
    {
        public static List<TenantViewModel> ApplicationDefaultTenants { get; } = new List<TenantViewModel>()
        {
            new TenantViewModel()
            {
                Name = "First Services Limited",
                Code = "FSL",
                IsActive = true,
                CanSeed = true,
                DateCreated = DateTime.Now,
                IsDeleted = false
            },

            new TenantViewModel()
            {
                Name = "Second Services Limited",
                Code = "SSL",
                IsActive = true,
                CanSeed = false,
                DateCreated = DateTime.Now,
                IsDeleted = false
            }
        };
    }
}
