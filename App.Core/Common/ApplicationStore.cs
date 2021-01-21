using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models;

namespace App.Core.Common
{
    public class ApplicationStore
    {
        public static string UserPassword { get; } = "password";
        public static List<MenuViewModel> ApplicationMenu { get; } = new List<MenuViewModel>()
        {
            //Dashboard
            new MenuViewModel(name: "Dashboard", url: "/Admin/Account/Index",  null, new List<MenuViewModel>(), icon: "la-tachometer", isPermission: false),
            //Account
            new MenuViewModel(name: "Account", url: "#", claimType: "Admin.Account", icon: "la-user-friends",
              childMenus: new List<MenuViewModel>
              {
                    new MenuViewModel("Permissions", "/Admin/Permission/Index", "Admin.Permission.Index", new List<MenuViewModel>()),
                    new MenuViewModel("Roles", "/Admin/Role/Index", "Admin.Role.Index", new List<MenuViewModel>()),
                    new MenuViewModel("Users", "/Admin/User/Index", "Admin.User.Index", new List<MenuViewModel>()),
              }),
            //Settings
            new MenuViewModel("Configuration", url: "#", "Admin.Configuration",icon: "la-cogs", childMenus: new List<MenuViewModel>()
            {
                new MenuViewModel(name: "Settings", url: "#", icon: "la-cog", claimType: "Admin.Configuration.Settings", childMenus:  new List<MenuViewModel>
              {
                    new MenuViewModel("General Settings", "/Admin/Configuration/Settings/General", "Admin.Configuration.Settings.General", new List<MenuViewModel>()),
              }),
            })
        };
    }

}
