using App.Core.Common;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Web.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Services
{
    public class MenuService : IMenuFactory
    {
        public List<MenuViewModel> GetMenus(List<MenuViewModel> allMenus = null, List<MenuViewModel> menus = null)
        {
            if (allMenus == null)
            {
                allMenus = new List<MenuViewModel>();
            }
            if (menus == null)
            {
                menus = ApplicationStore.ApplicationMenu;
            }
            foreach (MenuViewModel menu in menus)
            {
                allMenus.Add(menu);
                if (menu.childMenus.Count > 0)
                {
                    GetMenus(allMenus, menu.childMenus);
                }
            }
            return allMenus;
        }

        public List<MenuViewModel> GetPermissions(List<MenuViewModel> allMenus = null, List<MenuViewModel> menus = null)
        {
            if (allMenus == null)
            {
                allMenus = new List<MenuViewModel>();
            }
            if (menus == null)
            {
                menus = ApplicationStore.ApplicationMenu;
            }
            foreach (MenuViewModel menu in menus.Where(x => x.IsPermission == true))
            {
                allMenus.Add(menu);
                if (menu.childMenus.Count > 0)
                {
                    GetMenus(allMenus, menu.childMenus);
                }
            }
            return allMenus;
        }


        public List<MenuViewModel> GetPermissionMenu(List<PermissionViewModel> permissions, List<PermissionViewModel> rolePermissions)
        {
            List<MenuViewModel> menus = new List<MenuViewModel>();
            foreach (MenuViewModel menu in GetPermissions())
            {
                if (permissions.Any(x => x.Type.Equals(menu.Claim.Type)))
                {
                    var rolePermission = rolePermissions.Where(x => x.Type.Equals(menu.Claim.Type)).FirstOrDefault();
                    menu.IsActive = false;
                    if (rolePermission != null)
                    {
                        menu.IsActive = rolePermission.IsActive;
                    }
                    menus.Add(menu);
                }
            }
            return menus;
        }
    }
}
