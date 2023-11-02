using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biblioteka.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {   ///
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50, ErrorMessage = "Ime ne može biti duže od 50 karaktera.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50, ErrorMessage = "Prezime ne može biti duže od 50 karaktera.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        [StringLength(100, ErrorMessage = "Adresa ne može biti duža od 50 karaktera.")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Telefon je obavezan.")]
        [RegularExpression(@"\+?[0-9]*", ErrorMessage = "Neispravan format telefonskog broja.")]
        public string Telefon { get; set; }

        
        public byte[] ProfilnaFotografija { get; set; }
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}