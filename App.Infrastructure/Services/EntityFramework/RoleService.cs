using App.Core.Common;
using App.Core.Configurations;
using App.Core.Domain.Entities;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Core.Models.Inputs;
using App.Infrastructure.Factories;
using App.Infrastructure.Factories.EntityFramework;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.EntityFramework
{
    public class RoleService : IRoleFactory
    {
        private readonly ApplicationDbContext _context;
        public RoleService()
        {
            _context = new ApplicationDbContext();
        }

        public void AddPermissions(List<MenuViewModel> menus, long Id)
        {
            throw new NotImplementedException();
        }

        public RoleViewModel Create(RoleViewModel model, long? creatorID, long tenantID)
        {
            Role newRole = new Role()
            {
                Name = model.Name,
                Color = model.Color,
                CreatedByID = creatorID,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                TenantID = tenantID
            };

            _context.Roles.Add(newRole);
            _context.SaveChanges();
            model.ID = newRole.ID;
            return model;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermissions(long Id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string name, long tenantID, long? Id = null)
        {
            return _context.Roles.Any(r => r.Name.Equals(name) && r.TenantID == tenantID && (Id != null ? (r.ID != Id) : (true)));
        }

        public IEnumerable<RoleViewModel> GetAll(long tenantID)
        {
            var roles = _context.Roles.Where(r => r.IsDeleted == false && r.TenantID == tenantID).OrderByDescending(o => o.DateCreated).ToList();

            List<RoleViewModel> roleVMs = new Mapper(AutoMapperConfig.Configurations).Map<List<Role>, List<RoleViewModel>>(roles);
            return roleVMs;
        }

        public IEnumerable<RoleViewModel> GetAllActive(long tenantID)
        {
            var roles = _context.Roles.Where(r => r.IsDeleted == false && r.IsActive ==true && r.TenantID == tenantID).OrderByDescending(o => o.DateCreated).ToList();

            List<RoleViewModel> roleVMs = new Mapper(AutoMapperConfig.Configurations).Map<List<Role>, List<RoleViewModel>>(roles);
            return roleVMs;
        }

        public RoleViewModel GetById(long Id)
        {
            var role = _context.Roles.Where(x => x.IsDeleted == false && x.ID == Id).FirstOrDefault();
            RoleViewModel roleVM = new Mapper(AutoMapperConfig.Configurations).Map<Role, RoleViewModel>(role);
            return roleVM;
        }

        public List<PermissionViewModel> GetAssignedPermissions(long Id)
        {
            var permissions = _context.Permissions.Where(p => p.IsDeleted == false && p.IsActive == true && p.RolePermissions.Any(r => r.RoleID == Id)).ToList();
            List<PermissionViewModel> permissionVMs = new Mapper(AutoMapperConfig.Configurations).Map<List<Permission>, List<PermissionViewModel>>(permissions);
            return permissionVMs;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(RoleViewModel model, long userID)
        {
            //DeletePermissions(model.ID);
            var role = _context.Roles.Where(x => x.IsDeleted == false && x.ID == model.ID).FirstOrDefault();
            if (role != null)
            {
                role.Name = model.Name;
                role.IsActive = model.IsActive;
                role.DateModified = DateTime.Now;
                role.ModifiedByID = userID;
                role.Color = model.Color;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Role not found");
            }
        }

        public List<RoleViewModel> GetUserRoles(long Id)
        {
            var roles = _context.UserRoles.Where(x => x.IsDeleted == false && x.UserID == Id).Select(s => s.Role).ToList();
            List<RoleViewModel> roleVMs = new Mapper(AutoMapperConfig.Configurations).Map<List<Role>, List<RoleViewModel>>(roles);
            return roleVMs;
        }
    }
}
