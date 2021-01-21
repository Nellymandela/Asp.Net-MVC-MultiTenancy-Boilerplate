using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Entities
{
    public class RoleViewModel
    {
        public long ID { get; set; }
        [Required]
        [Display(Name = "Role name")]
        public string Name { get; set; }
        public string Color { get; set; }

        public string ConcurrencyStamp { get; set; }

        public DateTime? DateCreated { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public List<PermissionViewModel> AssignedPermissions { get; set; }
    }
}
