using App.Core.Domain.Entities;
using App.Core.Models;
using App.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Factories.EntityFramework
{
    public interface IUserFactory : IEntityFactory<UserViewModel>
    {
        User User { get; set; }

        /// <summary>
        /// Determines if user with email exists. Checks with Id if value is passed, else ignores Id
        /// </summary>
        /// <param name="email"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Exists(string email, long tenantID, long? Id = null);

        void DeletePermissions(long Id);

        void AddPermissions(List<MenuViewModel> menus, long Id);

        void AddRoles(List<long> roles, long Id, long userID);

        void DeleteRoles(long Id);

        List<MenuViewModel> GetPermissions(long Id, bool includeUserPermissions = false);

        void ChangePassword(UserViewModel model, string oldPassword, string newPassword);
    }
}
