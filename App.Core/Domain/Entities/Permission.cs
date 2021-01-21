using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Domain.Entities
{
    [Table(nameof(Permission))]
    public partial class Permission : EntityBase
    {
        [Index(IsUnique = true)]
        [StringLength(256)]
        public string Type { get; set; }

        public string Value { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
