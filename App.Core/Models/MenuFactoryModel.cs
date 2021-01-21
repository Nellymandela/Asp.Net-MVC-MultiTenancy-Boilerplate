using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;

namespace App.Core.Models
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {

        }

        public MenuViewModel(string name, string url, string claimType, List<MenuViewModel> childMenus, string icon = null, bool isPermission = true)
        {
            Name = name;
            Url = url;
            Icon = icon;
            Claim = (claimType != null ? new Claim(claimType, name) : null);
            this.childMenus = childMenus;
            IsPermission = isPermission;
            IsMenu = !string.IsNullOrEmpty(url);
        }

        public string Name { get; }
        public string Url { get; }
        public List<MenuViewModel> childMenus { get; set; }
        public string Icon { get; set; }
        /// <summary>
        /// Gets value to determine if menu can be seeded during Migration seed
        /// </summary>
        public bool IsPermission { get; }
        public bool IsActive { get; set; }
        public bool IsMenu { get; }
        public Claim Claim { get; set; }
    }
    public class MenuFactory
    {
        public static List<MenuViewModel> GetMenus(List<MenuViewModel> allMenus, List<MenuViewModel> menus)
        {
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

        public static List<MenuViewModel> GetPermissions(List<MenuViewModel> allMenus, List<MenuViewModel> menus)
        {
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
    }
}
