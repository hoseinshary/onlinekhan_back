﻿using System.ComponentModel.DataAnnotations;
using Onlinekhan.SSO.Common;

namespace Onlinekhan.SSO.ViewModels.Role
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "سطح")]
        public byte Level { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "نوع کاربری")]
        public UserType UserType { get; set; }
    }
}
