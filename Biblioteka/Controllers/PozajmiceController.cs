using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;
using Microsoft.AspNet.Identity;

namespace Biblioteka.Controllers
{
	public class PozajmiceController : Controller
	{
		private BibliotekaEntities db = new BibliotekaEntities();

		// GET: Pozajmice

		public ActionResult Index()
        {
			//string korisnickoIme = User.Identity.Name;

			//var pozajmice = db.Pozajmice.Include(p => p.Clanovi).Include(p => p.PrimjerciKnjiga).Include(p => p.PrimjerciKnjiga.IzdanjaKnjiga.Knjige);
			//return View(pozajmice.ToList());

			var pozajmice = db.Pozajmice
				  .Include(p => p.Clanovi)
				  .Include(p => p.PrimjerciKnjiga)
				  .Include(p => p.PrimjerciKnjiga.IzdanjaKnjiga.Knjige)
				  .Include(p => p.AspNetUsers);
			return View(pozajmice.ToList());

		}

		// GET: Pozajmice/Details/5
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

		

		// GET: Pozajmice/Create
		public ActionResult Create()
		{
			//ViewBag.ClanId = new SelectList(db.Clanovi, "Id", "Ime");
			ViewBag.ClanId = new SelectList(db.Clanovi.Select(c => new { Id = c.Id, ImePrezime = c.Ime + " " + c.Prezime }), "Id", "ImePrezime");
			ViewBag.KnjigeId = new SelectList(db.Knjige, "Id", "Naslov");
			ViewBag.PrimjerakKnjigeId = new SelectList(db.PrimjerciKnjiga, "Id", "Sifra");

			return View();
		}

		public JsonResult GetIzdavackeKuce(int knjigaId)
		{
			var izdanja = db.IzdanjaKnjiga.Where(i => i.KnjigeId == knjigaId).ToList();
			return Json(izdanja.Select(i => new { Id = i.Id, Naziv = i.IzdavackeKuce.Naziv}), JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetPrimjerciKnjiga(int izdanjeId)
		{
			var primjerci = db.PrimjerciKnjiga.Where(p => p.IzdanjeKnjigeId == izdanjeId && p.Zaduzen == false).ToList();
			return Json(primjerci.Select(p => new { Id = p.Id, Sifra = p.Sifra }), JsonRequestBehavior.AllowGet);
		}




		// POST: Pozajmice/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
		
		public ActionResult Create([Bind(Include = "Id, BibliotekarId, ClanId,PrimjerakKnjigeId,DatumPozajmice,DatumZakazanogVracanja,DatumVracanja")] Pozajmice pozajmice)
		{
			if (ModelState.IsValid)
			{
				pozajmice.BibliotekarId = User.Identity.GetUserId();

				db.Pozajmice.Add(pozajmice);
				db.SaveChanges();

				var primjerakKnjige = db.PrimjerciKnjiga.Find(pozajmice.PrimjerakKnjigeId);
				if (primjerakKnjige != null)
				{
					primjerakKnjige.Zaduzen = true;
					db.SaveChanges();
				}

				return RedirectToAction("Index");
			}

			ViewBag.ClanId = new SelectList(db.Clanovi.Select(c => new { Id = c.Id, ImePrezime = c.Ime + " " + c.Prezime }), "Id", "ImePrezime", pozajmice.ClanId);
			ViewBag.KnjigeId = new SelectList(db.Knjige, "Id", "Naslov");
			ViewBag.PrimjerakKnjigeId = new SelectList(db.PrimjerciKnjiga, "Id", "Sifra");

			return View(pozajmice);
		}

		//GET: Pozajmice/Edit/5
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
			ViewBag.ClanId = new SelectList(db.Clanovi, "Id", "Ime", pozajmice.ClanId);
			ViewBag.PrimjerakKnjigeId = new SelectList(db.PrimjerciKnjiga, "Id", "Sifra", pozajmice.PrimjerakKnjigeId);
			return View(pozajmice);
		}




        //POST: Pozajmice/Edit/5
   //     To protect from overposting attacks, enable the specific properties you want to bind to, for 
		 //more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BibliotekarId,ClanId,PrimjerakKnjigeId,DatumPozajmice,DatumZakazanogVracanja,DatumVracanja")] Pozajmice pozajmice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pozajmice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(db.Clanovi, "Id", "Ime", pozajmice.ClanId);
            ViewBag.PrimjerakKnjigeId = new SelectList(db.PrimjerciKnjiga, "Id", "Sifra", pozajmice.PrimjerakKnjigeId);
            return View(pozajmice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]



        // GET: Pozajmice/Delete/5
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

        // POST: Pozajmice/Delete/5
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
