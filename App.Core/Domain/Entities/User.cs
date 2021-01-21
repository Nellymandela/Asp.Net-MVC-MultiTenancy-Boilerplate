namespace App.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(User))]
    public partial class User : TenancyEntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserPermissions = new HashSet<UserPermission>();
            UserRoles = new HashSet<UserRole>();
        }

        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public long? CreatorUserID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string OtherName { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }


        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool IsPasswordRequired { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool IsSuperAdmin { get; set; }

        public bool IsDefault { get; set; }

        public byte[] Passport { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
