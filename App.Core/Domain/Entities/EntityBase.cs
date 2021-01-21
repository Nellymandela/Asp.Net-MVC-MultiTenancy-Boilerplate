using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Core.Domain.Entities
{
    public class EntityBase
    {
        public long ID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByID { get; set; }
        //[ForeignKey("CreatedByID")]
        //public virtual User CreatedBy { get; set; }
        public long? ModifiedByID { get; set; }
        //[ForeignKey("ModifiedByID")]
        //public virtual User ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}
