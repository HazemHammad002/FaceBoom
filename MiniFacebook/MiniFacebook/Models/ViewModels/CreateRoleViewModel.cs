using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        
        public string Id { get; set; }
       [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string RoleDescription { get; set; }
    }
}
