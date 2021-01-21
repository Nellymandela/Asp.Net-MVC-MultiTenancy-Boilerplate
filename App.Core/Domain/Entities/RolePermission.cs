namespace App.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(RolePermission))]
    public partial class RolePermission : EntityBase
    {
        [Required]
        public long RoleID { get; set; }

        [Required]
        public long PermissionID { get; set; }

        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}
