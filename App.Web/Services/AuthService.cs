using App.Core.Domain.Entities;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Core.Models.Inputs;
using App.Core.Security;
using App.Infrastructure;
using App.Web.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Services
{
    public class AuthService : Factories.IAuthFactory, IDisposable
    {

        public int MaxFailedAccessAttemptsBeforeLockout { get; } = 5;

        public int LockoutMinutes { get; } = 5;
        public User User { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public SignInResult SignIn(LoginViewModel model, bool shouldLockout = false)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.Users.Include("Tenant").Where(x => x.IsDeleted == false && x.UserName == model.Email && x.Tenant.Code.Equals(model.Tenant)).FirstOrDefault();
                if (user != null)
                {
                    if (user.PasswordHash.Equals(MD5Cryptography.EncryptWithMd5(model.Password)))
                    {
                        if (user.LockoutEnabled && user.LockoutEnd <= DateTimeOffset.Now)
                        {
                            user.LockoutEnabled = false;
                            user.LockoutEnd = null;
                            user.AccessFailedCount = 0;
                            context.SaveChanges();
                        }
                        if (user.LockoutEnabled)
                        {
                            return SignInResult.LockedOut;
                        }
                        else if (!user.IsActive)
                        {
                            return SignInResult.Inactive;
                        }
                        else if (user.TwoFactorEnabled)
                        {
                            return SignInResult.TwoFactorAuthRequired;
                        }
                        else
                        {
                            if (user.AccessFailedCount > 0)
                            {
                                user.LockoutEnabled = false;
                                user.LockoutEnd = null;
                                user.AccessFailedCount = 0;
                                context.SaveChanges();
                            }
                            User = user;
                            return SignInResult.Success;
                        }
                    }
                    else
                    {
                        if (shouldLockout)
                        {
                            user.AccessFailedCount++;
                            user.LockoutEnabled = (user.AccessFailedCount >= MaxFailedAccessAttemptsBeforeLockout);
                            user.LockoutEnd = DateTimeOffset.Now.AddMinutes(LockoutMinutes);
                            context.SaveChanges();
                        }
                        return SignInResult.Failed;
                    }
                }
                else
                {
                    return SignInResult.Failed;
                }
            }
        }

        public List<MenuViewModel> GetPermissions(long Id, bool includeUserPermissions = false)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.Users.Where(x => x.ID == Id).FirstOrDefault();
                var roles = user.UserRoles.Where(x => x.IsDeleted == false && x.Role.IsDeleted == false && x.IsActive == true).ToList();
                List<MenuViewModel> permissions = new List<MenuViewModel>();
                foreach (var role in roles)
                {
                    var rolePermissions = role.Role.RolePermissions.Where(x => x.IsDeleted == false && x.Permission.IsActive == true)
                        .Select(s => new MenuViewModel() { Claim = new System.Security.Claims.Claim(s.Permission.Type, s.Permission.Value) }).ToList();
                    permissions.AddRange(rolePermissions);
                }

                if (includeUserPermissions)
                {
                    var userPermissions = user.UserPermissions.Where(x => x.IsDeleted == false && x.Permission.IsActive == true)
                        .Select(s => new MenuViewModel() { Claim = new System.Security.Claims.Claim(s.Permission.Type, s.Permission.Value) });
                    permissions.AddRange(userPermissions);
                }
                return permissions;
            }
        }

        public List<RoleViewModel> GetRoles(long Id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var roles = context.UserRoles.Where(x => x.IsDeleted == false && x.UserID == Id).Select(s => s.Role).ToList();
                //List<RoleViewModel> roleVMs = new Mapper(AutoMapperConfig.Configurations).Map<List<Role>, List<RoleViewModel>>(roles);
                //return roleVMs;
                return new List<RoleViewModel>();
            }
        }
    }
}
