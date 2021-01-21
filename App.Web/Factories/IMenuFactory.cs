using App.Core.Models;
using App.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Factories
{
    interface IMenuFactory
    {
        List<MenuViewModel> GetMenus(List<MenuViewModel> allMenus, List<MenuViewModel> menus);

        List<MenuViewModel> GetPermissions(List<MenuViewModel> allMenus, List<MenuViewModel> menus);
        List<MenuViewModel> GetPermissionMenu(List<PermissionViewModel> permissions, List<PermissionViewModel> rolePermissions);
    }
}
