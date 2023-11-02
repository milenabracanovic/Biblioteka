using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.DTO
{
    public class ClanGetDTO
    {
        public int ClanID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string MaticniBroj { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }
    }
}