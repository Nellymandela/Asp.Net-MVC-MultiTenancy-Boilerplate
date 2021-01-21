using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Entities
{
    public class TenantViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long ID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool CanSeed { get; set; }
        public string Url { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Copyright { get; set; }
        public long PensionNoCounter { get; set; }

        public byte[] Logo { get; set; }
        public byte[] Favicon { get; set; }
        public long? ModifiedUserID { get; set; }
    }
}
