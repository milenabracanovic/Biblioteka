using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.DTO
{
    public class ClanSaveDTO
    {
        

        [Required(ErrorMessage = "Ime je obavezno polje.")]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno polje.")]
        [StringLength(50)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Matični broj je obavezno polje.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Matični broj mora imati tačno 13 karaktera.")]
        [Display(Name ="Matični broj")]
        public string MaticniBroj { get; set; }

        [Required(ErrorMessage = "Email je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese.")]
        public string Email { get; set; }

        [StringLength(50)]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Datum rođenja je obavezno polje.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum rođenja")]
        public System.DateTime DatumRodjenja { get; set; }
    }
}
