using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class RazduziController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Razduzi
        public ActionResult Index()
        {
			//var pozajmice = db.Pozajmice.Include(p => p.Clanovi).Include(p => p.PrimjerciKnjiga);
			//return View(pozajmice.ToList());
			var pozajmice = db.Pozajmice
	.Include(p => p.Clanovi)
	.Include(p => p.PrimjerciKnjiga)
	.Where(p => p.PrimjerciKnjiga.Zaduzen == true && p.DatumVracanja == null)
	.ToList();

			return View(pozajmice);

		}

		// GET: Razduzi/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozajmice pozajmice = db.Pozajmice.Find(id);
            if (pozajmice == null)
            {
                return HttpNotFound();
            }
            return View(pozajmice);
        }

        // GET: Razduzi/Create
        public ActionResult Create()
        {
            ViewBag.ClanId = new SelectList(db.Clanovi, "Id", "Ime");
            ViewBag.PrimjerakKnjigeId = new SelectList(db.PrimjerciKnjiga, "Id", "Sifra");
            return View();
        }

        // POST: Razduzi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BibliotekarId,ClanId,PrimjerakKnjigeId,DatumPozajmice,DatumZakazanogVracanja,DatumVracanja")] Pozajmice pozajmice)
        {
            if (ModelState.IsValid)
            {
                db.Pozajmice.Add(pozajmice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(db.Clanovi, "Id", "Ime", pozajmice.ClanId);
            ViewBag.PrimjerakKnjigeId = new SelectList(db.PrimjerciKnjiga, "Id", "Sifra", pozajmice.PrimjerakKnjigeId);
            return View(pozajmice);
        }

		

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Pozajmice pozajmice = db.Pozajmice.Find(id);
			if (pozajmice == null)
			{
				return HttpNotFound();
			}
			return View(pozajmice);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,DatumVracanja")] Pozajmice pozajmice)
		{
			if (ModelState.IsValid)
			{
				
				Pozajmice originalPozajmice = db.Pozajmice.Find(pozajmice.Id);

				if (originalPozajmice == null)
				{
					return HttpNotFound();
				}
				// Ažuriranje polja Datum vracanja
				originalPozajmice.DatumVracanja = pozajmice.DatumVracanja;
				db.SaveChanges();
				var primjerakKnjige = db.PrimjerciKnjiga.SingleOrDefault(p => p.Id == originalPozajmice.PrimjerakKnjigeId);


				if (primjerakKnjige != null && originalPozajmice.DatumVracanja != null)
				{
					primjerakKnjige.Zaduzen = false;
					db.SaveChanges();
				}



				return RedirectToAction("Index");
			}
			return View(pozajmice);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
	

		// GET: Razduzi/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozajmice pozajmice = db.Pozajmice.Find(id);
            if (pozajmice == null)
            {
                return HttpNotFound();
            }
            return View(pozajmice);
        }

        // POST: Razduzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pozajmice pozajmice = db.Pozajmice.Find(id);
            db.Pozajmice.Remove(pozajmice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
