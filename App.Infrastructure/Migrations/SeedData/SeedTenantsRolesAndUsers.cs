using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Migrations.SeedData
{
    public class SeedTenantsRolesAndUsers : DataSeedBase
    {
        private readonly List<Core.Domain.Entities.Tenant> _tenants;
        public SeedTenantsRolesAndUsers(ApplicationDbContext context, List<Core.Domain.Entities.Tenant> tenants)
        {
            _context = context;
            _tenants = tenants;
        }

        public void Seed()
        {
            foreach (Core.Domain.Entities.Tenant tenant in _tenants)
            {
                var role = SeedRoles(_context, tenant.ID);
                SeedRolePermissions(_context, role.ID);
                var user = SeedUsers(_context, tenant.ID);
                SeedUserRoles(_context, user.ID, role.ID);
            }
        }
        private Core.Domain.Entities.Role SeedRoles(ApplicationDbContext context, long TenantID)
        {
            if (!context.Roles.Any(r => r.TenantID == TenantID && r.IsDefault == true))
            {
                Core.Domain.Entities.Role role = new Core.Domain.Entities.Role()
                {
                    Name = "Default Role",
                    TenantID = TenantID,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    IsDefault = true,
                    IsDeleted = false
                };
                context.Roles.Add(role);
                context.SaveChanges();
                return role;
            }
            else
            {
                return context.Roles.FirstOrDefault(r => r.TenantID == TenantID && r.IsDefault == true);
            }

        }

        private void SeedRolePermissions(ApplicationDbContext context, long roleID)
        {
            var permissions = context.Permissions.Where(x => x.IsDeleted == false && x.IsActive == true).ToList();
            foreach (var permission in permissions)
            {
                if (!context.RolePermissions.Any(x => x.Permission.Type == permission.Type && x.RoleID == roleID))
                {
                    context.RolePermissions.Add(new Core.Domain.Entities.RolePermission()
                    {
                        RoleID = roleID,
                        PermissionID = permission.ID,
                        IsActive = true,
                        IsDeleted = false,
                        DateCreated = DateTime.Now
                    });
                }
            }
            context.SaveChanges();
        }

        private Core.Domain.Entities.User SeedUsers(ApplicationDbContext context, long TenantID)
        {
            if (!context.Users.Any(u => u.TenantID == TenantID && u.IsDefault == true))
            {
                string email = "default@bluetag.com";
                Core.Domain.Entities.User user = new Core.Domain.Entities.User()
                {
                    Email = email,
                    LastName = "Default",
                    FirstName = "User",
                    TenantID = TenantID,
                    UserName = email,
                    EmailConfirmed = true,
                    PasswordHash = Core.Security.MD5Cryptography.EncryptWithMd5(Core.Common.ApplicationStore.UserPassword),
                    PhoneNumberConfirmed = true,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    IsDefault = true,
                    IsDeleted = false
                };
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
            else
            {
                return context.Users.FirstOrDefault(u => u.TenantID == TenantID && u.IsDefault == true);
            }
        }

       private void SeedUserRoles(ApplicationDbContext context, long UserID, long RoleID)
        {
            if (!context.UserRoles.Any(u => u.UserID == UserID && u.RoleID == RoleID && u.IsDeleted == false && u.IsActive == true))
            {
                Core.Domain.Entities.UserRole userRole = new Core.Domain.Entities.UserRole()
                {
                    RoleID = RoleID,
                    UserID = UserID,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                };
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }
        }
    }
}
