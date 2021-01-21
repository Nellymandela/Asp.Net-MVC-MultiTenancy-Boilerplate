using App.Core.Models;
using App.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Factories.EntityFramework
{
    public interface IPermissionFactory : IDisposable
    {
        List<PermissionViewModel> GetAll();

        /// <summary>
        /// Compare permissions and menus then set ExistAspermission to true for any menu that exists in permissions
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        List<MenuViewModel> GetPermissionMenu(List<PermissionViewModel> permissions, List<MenuViewModel> menus);

        void UpdatePermission(List<MenuViewModel> menus);

        bool Exists(string type);
    }
}
