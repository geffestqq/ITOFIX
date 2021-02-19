using Diplom.Models;
using System.ComponentModel.DataAnnotations;

namespace Diplom.ViewModels
{
    public class AccountViewModel
    {
        public string Id { get; set; }


        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int? PluralityId { get; set; }

        public string PluralityView { get; set; }
    }
}
