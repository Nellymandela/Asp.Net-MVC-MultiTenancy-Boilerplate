using App.Core.Models.Entities.Tenant;
using App.Web.Core.Models.UI.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Core.Configurations.Application
{
    public class ApplicationStore
    {
        public static readonly string deceased_pensioner_flag = "Deceased";
        public static readonly string living_pensioner_flag = "Living";

        public static List<MenuViewModel> ApplicationMenu { get; } = new List<MenuViewModel>()
        {
            //Dashboard
            new MenuViewModel(name: "Dashboard", url: "/Admin/Account/Index",  null, new List<MenuViewModel>(), icon: "icon-home", isPermission: false),
            //Account
            new MenuViewModel(name: "Account", url: "#", claimType: "Admin.Account", icon: "icon-users",
              childMenus: new List<MenuViewModel>
              {
                    new MenuViewModel("New User", "/Admin/User/UserSetup", "Admin.User.UserSetup", new List<MenuViewModel>()),
                    new MenuViewModel("Edit User", null, "Admin.User.Edit", new List<MenuViewModel>()),
                    new MenuViewModel("Manage Users", "/Admin/User/Index", "Admin.User.Index", new List<MenuViewModel>()),

                    new MenuViewModel("Role Setup", null, "Admin.Role.RoleSetup", new List<MenuViewModel>()),
                    new MenuViewModel("Edit Role", null, "Admin.Role.Edit", new List<MenuViewModel>()),
                    new MenuViewModel("Roles", "/Admin/Role/Index", "Admin.Role.Index", new List<MenuViewModel>()),

                    new MenuViewModel("Edit Permission", null, "Admin.Permission.Edit", new List<MenuViewModel>()),
                    new MenuViewModel("Permissions", "/Admin/Permission/Index", "Admin.Permission.Index", new List<MenuViewModel>())
              }),

            //Enrollment
            new MenuViewModel(name: "Enrollment", url: "#", claimType: "Admin.Enrollment", icon: "icon-vcard",
              childMenus: new List<MenuViewModel>
              {
                    new MenuViewModel("New Enrollment", "/Admin/Enrollment/NewEnrollment", "Admin.Enrollment.NewEnrollment", new List<MenuViewModel>())
              }),

            //Pensioner
            new MenuViewModel(name: "Pensioner", url: "#", claimType: "Admin.Pensionier", icon: "icon-user-tie",
              childMenus: new List<MenuViewModel>
              {
                    new MenuViewModel("Manage Pensionier", "/Admin/Pensionier/Index", "Admin.Pensionier.ManagePensionier", new List<MenuViewModel>()
                    {
                        new MenuViewModel("Edit Employment Details", null, "Admin.Pensionier.Edit.EmploymentDetails", new List<MenuViewModel>()),
                        new MenuViewModel("Edit Personal Details", null, "Admin.Pensionier.Edit.PersonalDetails", new List<MenuViewModel>()),
                        new MenuViewModel("Edit Bank Details", null, "Admin.Pensionier.Edit.BankDetails", new List<MenuViewModel>()),
                        new MenuViewModel("Edit Pension Details", null, "Admin.Pensionier.Edit.PensionDetails", new List<MenuViewModel>()),
                        new MenuViewModel("Edit Pension Service Records", null, "Admin.Pensionier.Edit.ServiceRecords", new List<MenuViewModel>()),
                        new MenuViewModel("Edit NOK Details", null, "Admin.Pensionier.Edit.NOKDetails", new List<MenuViewModel>()),
                        new MenuViewModel("Edit Documents", null, "Admin.Pensionier.Edit.PensionerDocuments", new List<MenuViewModel>()),
                        new MenuViewModel("Create Next-Of-Kin", null, "Admin.Pensionier.CreateNOK", new List<MenuViewModel>()),
                        new MenuViewModel("Create Beneficiary", null, "Admin.Pensionier.CreateNokBeneficiaryRequest", new List<MenuViewModel>())
                    }),
                    new MenuViewModel("NOK Change Request", "/Admin/Pensionier/NOKChangeRequest", "Admin.Pensionier.NOKChangeRequest", new List<MenuViewModel>()),
                    new MenuViewModel("Manage Beneficiary Request", "/Admin/NokBeneficiaryRequest/Index", "Admin.Pensionier.NokBeneficiaryRequest", new List<MenuViewModel>())
              }),

            //Reports
            new MenuViewModel("Reports", url: "#", "Admin.Reports", icon: "icon-files-empty", childMenus: new List<MenuViewModel>()
            {
                new MenuViewModel("Enrollment", "/Admin/Report/Enrollment", "Admin.Pensionier.Report.Enrollment", new List<MenuViewModel>()),
                new MenuViewModel("Application Summary", "/Admin/Report/ApplicationSummary", "Admin.Pensionier.Report.ApplicationSummary", new List<MenuViewModel>()),
                new MenuViewModel("Payment", "/Admin/Report/Payment", "Admin.Pensionier.Report.Payment", new List<MenuViewModel>()),
            }),

            //Settings
            new MenuViewModel("Settings", url: "#", "Admin.Settings", icon: "icon-cog", childMenus: new List<MenuViewModel>()
            {
                new MenuViewModel("Application Settings", "/Admin/Settings/ApplicationSettings", "Admin.Pensionier.Settings.ApplicationSettings", new List<MenuViewModel>()),
            })
        };

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

        /// <summary>
        /// Gets all application default tenants, this property value cannot be set.
        /// </summary>
        public static List<TenantViewModel> ApplicationDefaultTenants { get; } = new List<TenantViewModel>()
        {
            new TenantViewModel()
            {
                Name = "First Services Limited",
                Code = "FSL",
                IsActive = true,
                CanSeed = true,
                DateCreated = DateTime.Now,
                IsDeleted = false
            },

            new TenantViewModel()
            {
                Name = "Second Services Limited",
                Code = "SSL",
                IsActive = true,
                CanSeed = false,
                DateCreated = DateTime.Now,
                IsDeleted = false
            }
        };
    }
}
