using App.Core.Models.Entities.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.Web.Core.Models.Entities.User
{
    public class UserViewModel
    {
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Select user roles")]
        public List<long> SelectedRoles { get; set; }

        public SelectList Roles { get; set; }

        public long ID { get; set; }

        public string UserName { get; set; }

        public long? CreatorUserID { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        public List<RoleViewModel> RoleVMs { get; set; }


        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public bool IsPasswordRequired { get; set; }

        public int AccessFailedCount { get; set; }


        public bool IsSuperAdmin { get; set; }

        public bool IsDefault { get; set; }

        public byte[] Passport { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long TenantID { get; set; }
    }
}
