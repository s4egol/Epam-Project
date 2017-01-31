using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class FullUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string NickName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [Compare("Password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime JoinTime { get; set; }

        [Required]
        [Display(Name = "Enter the captcha")]
        public string Captcha { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}