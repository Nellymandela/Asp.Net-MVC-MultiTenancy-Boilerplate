using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Core.Models.ViewModels.Role
{
    public class RoleSetupViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
