namespace App.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(Role))]
    public partial class Role : TenancyEntityBase
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserRoles = new HashSet<UserRole>();
        }

        [StringLength(256)]
        public string Name { get; set; }
        public string Color { get; set; }

        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
