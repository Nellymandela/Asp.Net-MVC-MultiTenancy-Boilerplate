namespace App.Core.Domain.Entities
{         
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(UserPermission))]
    public partial class UserPermission :EntityBase
    {
        [Required]
        public long UserID { get; set; }

        [Required]
        public long PermissionID { get; set; }

        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
