using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.DTO
{
    public class NajnovijaIzdanjaDTO
    {
        public int IzdanjeID { get; set; }
        public int Godina { get; set; }
        public string IzdavackaKuca { get; set; }
        //za sliku cu jos vidjeti sta i kako
        public string SlikaKorica { get; set; }
        public string Knjiga { get; set; }
        public int BrojNaStanju { get; set; }
        public int BrojIzdatih {  get; set; }

      
    }
}