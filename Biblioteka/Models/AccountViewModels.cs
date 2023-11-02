using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Biblioteka.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [StringLength(100, ErrorMessage = "{0} mora biti minimum {2} karaktera .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Ime je obavezno.")]
       
        [StringLength(50, ErrorMessage = "Ime ne može biti duže od 50 karaktera.")]
        ///////////////////////////////////////////////////////////////////////////
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50, ErrorMessage = "Prezime ne može biti duže od 50 karaktera.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        [StringLength(100, ErrorMessage = "Adresa ne može biti duža od 100 karaktera.")]
        public string Adresa{ get; set; }

        [Required(ErrorMessage = "Telefon je obavezan.")]
        [RegularExpression(@"\+?[0-9]*", ErrorMessage = "Neispravan format telefonskog broja.")]
        public string Telefon { get; set; }

        
        public HttpPostedFileBase ProfilnaFotografija { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
