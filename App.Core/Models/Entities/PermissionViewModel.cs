using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Entities
{
    public class PermissionViewModel
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
