using App.Infrastructure.Factories.EntityFramework;
using System;
using App.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Core.Configurations;
using AutoMapper;
using App.Core.Security;
using App.Core.Common;

namespace App.Infrastructure.Services.EntityFramework
{
    public class UserService : IUserFactory
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddPermissions(List<MenuViewModel> menus, long Id)
        {
            throw new NotImplementedException();
        }

        public void AddRoles(List<long> roles, long Id, long userID)
        {
            foreach (var role in roles)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    UserID = Id,
                    RoleID = role,
                    CreatedByID = userID,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                });
            }
            _context.SaveChanges();
        }

        public void ChangePassword(UserViewModel model, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public UserViewModel Create(UserViewModel model, long? creatorID, long tenantID)
        {
            var user = new User()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                IsPasswordRequired = true,
                IsSuperAdmin = false,
                UserName = model.Email,
                PasswordHash = MD5Cryptography.EncryptWithMd5(ApplicationStore.UserPassword),
                CreatorUserID = creatorID,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                TenantID = tenantID
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            var userVM = GetById(user.ID);
            return userVM;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermissions(long Id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoles(long Id)
        {
            var roles = _context.UserRoles.Where(x => x.UserID == Id).ToList();
            _context.UserRoles.RemoveRange(roles);
            _context.SaveChanges();
        }

        public bool Exists(string email, long tenantID, long? Id = null)
        {
            return _context.Users.Any(r => r.IsDeleted == false && r.Email.Equals(email) && r.TenantID == tenantID && (Id != null ? (r.ID != Id) : (true)));
        }

        public IEnumerable<UserViewModel> GetAll(long tenantID)
        {
            var userVMs = _context.Users.Where(r => r.IsDeleted == false && r.TenantID == tenantID).OrderByDescending(o => o.DateCreated).Select(s => new UserViewModel()
            {
                LastName = s.LastName,
                FirstName = s.FirstName,
                OtherName = s.OtherName,
                CreatorUserID = s.CreatorUserID,
                DateCreated = s.DateCreated,
                AccessFailedCount = s.AccessFailedCount,
                ConcurrencyStamp = s.ConcurrencyStamp,
                IsPasswordRequired = s.IsPasswordRequired,
                IsSuperAdmin = s.IsSuperAdmin,
                LockoutEnabled = s.LockoutEnabled,
                Email = s.Email,
                EmailConfirmed = s.EmailConfirmed,
                DateModified = s.DateModified,
                ID = s.ID,
                IsActive = s.IsActive,
                IsDefault = s.IsDefault,
                IsDeleted = s.IsDeleted,
                LockoutEnd = s.LockoutEnd,
                TenantID = s.TenantID,
                Passport = s.Passport,
                PasswordHash = s.PasswordHash,
                PhoneNumber = s.PhoneNumber,
                PhoneNumberConfirmed = s.PhoneNumberConfirmed,
                SecurityStamp = s.SecurityStamp,
                TwoFactorEnabled = s.TwoFactorEnabled,
                UserName = s.UserName,
                RoleVMs = s.UserRoles.Where(r => r.IsDeleted == false).Select(s2 => new RoleViewModel()
                {
                    Name = s2.Role.Name,
                    ID = s2.ID,
                    Color = s2.Role.Color,
                    IsDefault = s2.Role.IsDefault
                }).ToList()
            }).ToList();
            return userVMs;
        }

        public IEnumerable<UserViewModel> GetAllActive(long tenantID)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetById(long Id)
        {
            var user = _context.Users.Where(x => x.IsDeleted == false && x.ID == Id).FirstOrDefault();
            if (user != null)
            {
                UserViewModel userVM = new Mapper(AutoMapperConfig.Configurations).Map<User, UserViewModel>(user);
                return userVM;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public List<MenuViewModel> GetPermissions(long Id, bool includeUserPermissions = false)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(UserViewModel model, long userID)
        {
            DeleteRoles(model.ID);
            AddRoles(model.SelectedRoles, model.ID, userID);
            var user = _context.Users.Where(x => x.IsDeleted == false && x.ID == model.ID).FirstOrDefault();
            if (user != null)
            {
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.OtherName = model.OtherName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.IsActive = model.IsActive;
                user.DateModified = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
