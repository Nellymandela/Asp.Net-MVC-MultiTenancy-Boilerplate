using App.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models.Entities;

namespace App.Infrastructure.Factories.EntityFramework
{
    public interface ITenantFactory : IEntityFactory<TenantViewModel>
    {
        TenantViewModel GetByCode(string code);
    }
}
