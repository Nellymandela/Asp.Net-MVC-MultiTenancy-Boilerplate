using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Factories
{
    interface IAuthFactory
    {
        Core.Domain.Entities.User User { get; set; }
        SignInResult SignIn(Core.Models.Inputs.LoginViewModel model, bool shouldLockout = false);
        List<Core.Models.MenuViewModel> GetPermissions(long Id, bool includeUserPermissions = false);
        List<Core.Models.Entities.RoleViewModel> GetRoles(long Id);
    }
    public enum SignInResult { Success, LockedOut, TwoFactorAuthRequired, Failed, Inactive };
}
