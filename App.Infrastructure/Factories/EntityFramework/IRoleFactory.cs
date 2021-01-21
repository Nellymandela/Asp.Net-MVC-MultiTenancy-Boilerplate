using App.Core.Domain.Entities;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Core.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Factories.EntityFramework
{
    public interface IRoleFactory : IEntityFactory<RoleViewModel>
    {
        /// <summary>
        /// Determines if role with name exists for a tenant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tenantID"></param>
        /// <returns>bool</returns>
        bool Exists(string name, long tenantID, long? Id = null);

        /// <summary>
        /// Delete all permissions associated to role with given Id
        /// </summary>
        /// <param name="Id"></param>
        void DeletePermissions(long Id);

        /// <summary>
        /// Adds given menus as permisssions for role with given Id
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="Id"></param>
        void AddPermissions(List<MenuViewModel> menus, long Id);

        /// <summary>
        /// returns all asigned permissions for role with the given Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<PermissionViewModel> GetAssignedPermissions(long Id);

        /// <summary>
        /// returns all asigned roles for user with given Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<RoleViewModel> GetUserRoles(long Id);
    }
}
