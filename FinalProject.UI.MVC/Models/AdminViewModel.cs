using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FinalProject.UI.MVC.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }       

        public IEnumerable<SelectListItem> RolesList { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "* First Name is required")]
        [StringLength(50, ErrorMessage = "* First Name must not exceed 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Last Name is required")]
        [StringLength(50, ErrorMessage = "* Last Name must not exceed 50 characters")]
        public string LastName { get; set; }
    }
}