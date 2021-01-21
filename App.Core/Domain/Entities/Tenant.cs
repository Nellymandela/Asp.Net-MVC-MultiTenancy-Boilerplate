using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Domain.Entities
{
    [Table(nameof(Tenant))]
    public partial class Tenant : EntityBase
    {
        public Tenant()
        {
            Users = new HashSet<User>();
            Roles = new HashSet<Role>();
        }

        [StringLength(256)]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Copyright { get; set; }

        public byte[] Logo { get; set; }
        public byte[] Favicon { get; set; }
        public string FaviconUrl { get; set; }

        [Index(IsUnique = true)]
        [StringLength(256)]
        public string Code { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
