using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Factories
{
    public interface IEntityFactory<TModel>
    {
        IEnumerable<TModel> GetAll(long tenantID);
        IEnumerable<TModel> GetAllActive(long tenantID);
        TModel GetById(long Id);
        TModel Create(TModel model, long? creatorID, long tenantID);
        void Update(TModel model, long userID);
        void Delete(int Id);
        void Save();
    }
}
