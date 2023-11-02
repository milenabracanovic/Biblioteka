using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class HomeController : Controller
    {
        private BibliotekaEntities _context;

        public HomeController()
        {
            _context = new BibliotekaEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            int trenutnaGodina = DateTime.Now.Year;
            int proslaGodina = trenutnaGodina - 1;
            DateTime trenutniDatum = DateTime.Today;

            int brojNajnovijihIzdanja = _context.IzdanjaKnjiga
                                        .Where(x => x.Godina >= proslaGodina)
                                        .Count();

            int brojPrekoracenja = _context.Pozajmice
                                    .Where(x => x.DatumZakazanogVracanja < trenutniDatum && (x.DatumVracanja == null || x.DatumVracanja > x.DatumZakazanogVracanja))
                                    .Count();

            ViewBag.BrojPrekoracenja = brojPrekoracenja;
            ViewBag.BrojNajnovijihIzdanja = brojNajnovijihIzdanja;

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}