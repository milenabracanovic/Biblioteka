using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.ViewModels
{
	public class PozajmiceViewModel
	{
		public int Id { get; set; }


		//public string BibliotekarId { get; set; }  // Strani ključ za AspNetUsers
		//public string ImePrezimeBibliotekara { get; set; }
		//public AspNetUsers AspNetUsers { get; set; }

		public string BibliotekarId { get; set; }
		public string ImePrezimeBibliotekara { get; set; }
		public virtual AspNetUsers AspNetUsers { get; set; }
		
		public int ClanId { get; set; }
		public ClanoviViewModel Clanovi { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public string ImePrezimeClana { get; set; }
		public int KnjigaId { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public KnjigeViewModel Knjige { get; set; }
		public int IzdanjeKnjigeId { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public IzdanjaKnjiga IzdanjaKnjiga { get; set; }
		public int PrimjerakKnjigeId { get; set; }
		public PrimjerciKnjigaViewModel PrimjerciKnjiga { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public string SifraPrimerkaKnjige { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public System.DateTime DatumPozajmice { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public System.DateTime DatumZakazanogVracanja { get; set; }
		[Required(ErrorMessage = "Obavezno polje.")]
		public Nullable<System.DateTime> DatumVracanja { get; set; }


	}
}