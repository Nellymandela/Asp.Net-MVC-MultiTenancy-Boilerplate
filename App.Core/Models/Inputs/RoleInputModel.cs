using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Inputs
{
    public class RoleInputModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
