using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Models.UI.Admin
{
    public class BreadCrumbViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public bool IsFilter { get; set; }
    }
}
