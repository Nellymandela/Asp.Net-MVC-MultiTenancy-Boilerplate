using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    [Table(nameof(UserRole))]
    public class UserRole : EntityBase

    {
        public long UserID { get; set; }

        public long RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
