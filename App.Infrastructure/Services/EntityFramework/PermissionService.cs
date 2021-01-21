using App.Core.Configurations;
using App.Core.Domain.Entities;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Infrastructure.Factories.EntityFramework;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.EntityFramework
{
    public class PermissionService : IPermissionFactory
    {
        private readonly ApplicationDbContext _context;
        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Exists(string type)
        {
            throw new NotImplementedException();
        }

        public List<PermissionViewModel> GetAll()
        {
            var permissions = _context.Permissions.Where(x => x.IsDeleted == false & x.IsActive == true).ToList();
            List<PermissionViewModel> permissionVMs = new Mapper(AutoMapperConfig.Configurations).Map<List<Permission>, List<PermissionViewModel>>(permissions);
            return permissionVMs;
        }

        public List<MenuViewModel> GetPermissionMenu(List<PermissionViewModel> permissions, List<MenuViewModel> menus)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermission(List<MenuViewModel> menus)
        {
            throw new NotImplementedException();
        }
    }
}
