using App.Core.Domain.Entities;
using App.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.EntityFramework
{
    public class TenantService : Factories.EntityFramework.ITenantFactory
    {
        private readonly ApplicationDbContext _context;
        public TenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TenantViewModel Create(TenantViewModel model, long? creatorID, long tenantID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TenantViewModel> GetAll(long tenantID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TenantViewModel> GetAllActive(long tenantID)
        {
            throw new NotImplementedException();
        }

        public TenantViewModel GetByCode(string code)
        {
                return _context.Tenants.Where(t => t.Code.Equals(code) && t.IsDeleted == false).Select(s => new TenantViewModel()
                {
                    Name = s.Name,
                    Code = s.Code,
                    ID = s.ID,
                    DateCreated = s.DateCreated,
                    DateModified = s.DateModified,
                    IsActive = s.IsActive,
                    IsDeleted = s.IsDeleted,
                    Url = s.Url,
                    Website = s.Website,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    Copyright = s.Copyright,
                    Logo = s.Logo,
                    Favicon = s.Favicon,
                    ModifiedUserID = s.ModifiedByID
                }).FirstOrDefault();
        }

        public TenantViewModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TenantViewModel model, long userID)
        {
            throw new NotImplementedException();
        }
    }
}
