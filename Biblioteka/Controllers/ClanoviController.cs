using Biblioteka.DTO;
using Biblioteka.Models;
using Biblioteka.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using Microsoft.AspNet.Identity;
using System.Net.Http;

namespace Biblioteka.Controllers
{
    [Authorize]
    public class ClanoviController : Controller
    {
        private BibliotekaEntities _context;

        public ClanoviController()
        {
            _context = new BibliotekaEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

       
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageNumber = (page ?? 1);

            var clanoviDTO = _context.Clanovi.Select(c => new ClanGetDTO
            {
                ClanID = c.Id,
                Ime = c.Ime,
                Prezime = c.Prezime,
                MaticniBroj = c.MaticniBroj,
                Email = c.Email,
                Adresa = c.Adresa,
                DatumRodjenja = c.DatumRodjenja
            })
            .OrderByDescending(c => c.ClanID)
            .ToPagedList(pageNumber, pageSize);

            return View(clanoviDTO);
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ClanSaveDTO clanSaveDTO)
        {

            var postojeciClan = _context.Clanovi.SingleOrDefault(c => c.MaticniBroj == clanSaveDTO.MaticniBroj);
            if (postojeciClan != null)
            {
                ModelState.AddModelError("MaticniBroj", "Matični broj već postoji.");
            }
            if (ModelState.IsValid)
            {
                
                var clan = new Clanovi
                {
                    Ime = clanSaveDTO.Ime,
                    Prezime = clanSaveDTO.Prezime,
                    MaticniBroj = clanSaveDTO.MaticniBroj,
                    Email = clanSaveDTO.Email,
                    Adresa = clanSaveDTO.Adresa,
                    DatumRodjenja = clanSaveDTO.DatumRodjenja
                };

                _context.Clanovi.Add(clan);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(clanSaveDTO);
        }

       
        public ActionResult Edit(int id)
        {
            var clan = _context.Clanovi.SingleOrDefault(c => c.Id == id);
            //var clan = _context.Clanovi.Find(id);

            if (clan == null)
                return HttpNotFound();

            var clanDto = new ClanSaveDTO
            {
                
                Ime = clan.Ime,
                Prezime = clan.Prezime,
                MaticniBroj = clan.MaticniBroj,
                Email = clan.Email,
                Adresa = clan.Adresa,
                DatumRodjenja = clan.DatumRodjenja
            };

            return View(clanDto);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClanSaveDTO clanDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", clanDto);
            }

            var clanInDb = _context.Clanovi.SingleOrDefault(c => c.Id == id); 

            if (clanInDb == null)
                return HttpNotFound();

            clanInDb.Ime = clanDto.Ime;
            clanInDb.Prezime = clanDto.Prezime;
            clanInDb.MaticniBroj = clanDto.MaticniBroj;
            clanInDb.Email = clanDto.Email;
            clanInDb.Adresa = clanDto.Adresa;
            clanInDb.DatumRodjenja = clanDto.DatumRodjenja;

            _context.SaveChanges();

            return RedirectToAction("Index", "Clanovi");
        }


        public ActionResult Delete(int id)
        {
            var clan = _context.Clanovi.SingleOrDefault(c => c.Id == id);

            if (clan == null)
            {  return HttpNotFound(); }
                
            var clanById = new ClanGetDTO
            {
                ClanID = clan.Id,
                Ime = clan.Ime,
                Prezime = clan.Prezime,
                Adresa = clan.Adresa,
                DatumRodjenja= clan.DatumRodjenja,
                Email = clan.Email,
                MaticniBroj= clan.MaticniBroj
            };
            return View(clanById);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var clan = _context.Clanovi.SingleOrDefault(c => c.Id == id);

            if (clan == null)
            {

                return HttpNotFound();
            }

            _context.Clanovi.Remove(clan);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
