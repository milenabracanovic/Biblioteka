using Biblioteka.DTO;
using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI;
using PagedList;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class IzdanjeKnjigeController : Controller
    {
        private BibliotekaEntities _context;

        public IzdanjeKnjigeController()
        {
            _context = new BibliotekaEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: IzdanjeKnjige
        public ActionResult ListaIzdanja(int? page)
        {
            int pageSize = 6; // broj redova po stranici
            int pageNumber = (page ?? 1);

            int trenutnaGodina = DateTime.Now.Year;
            int proslaGodina = trenutnaGodina - 1;

            var izdanja = _context.IzdanjaKnjiga
                .Include(x => x.IzdavackeKuce)
                .Include(x => x.Knjige)
                .Where(x => x.Godina >= proslaGodina)
                .OrderBy(x => x.Knjige.Naslov)
                .Select(x => new NajnovijaIzdanjaDTO
                {
                    IzdanjeID = x.Id,
                    Godina = x.Godina,
                    IzdavackaKuca = x.IzdavackeKuce.Naziv,
                    Knjiga = x.Knjige.Naslov,
                    SlikaKorica = x.SlikaKorica,
                    BrojNaStanju = x.Knjige.BrojNaStanju,
                    BrojIzdatih = x.Knjige.BrojIzdatih,
                })
                .ToPagedList(pageNumber, pageSize);

            //var izdanjaResult = _context.NajnovijaIzdanjaKnjiga(proslaGodina).ToList();
            //var izdanja = izdanjaResult
            //    .Select(x => new NajnovijaIzdanjaDTO
            //    {
            //        IzdanjeID = x.Id,
            //        Godina = x.Godina,
            //        IzdavackaKuca = x.Naziv,
            //        Knjiga = x.Naslov,
            //        SlikaKorica = x.SlikaKorica,
            //        BrojNaStanju = x.BrojNaStanju,
            //        BrojIzdatih = x.BrojIzdatih,
            //    }).ToList();
            //NajnovijaIzdanjaKnjiga_Result

            //var izdanja = _context.IzdanjaKnjiga
            //.Where(x => x.Godina >= proslaGodina)
            //.OrderBy(x => x.Knjige.Naslov)
            //.Select(x => new NajnovijaIzdanjaDTO
            //{
            //    IzdanjeID = x.Id,
            //    Godina = x.Godina,
            //    IzdavackaKuca = x.IzdavackeKuce.Naziv,
            //    Knjiga = x.Knjige.Naslov,
            //    SlikaKorica = x.SlikaKorica,
            //    BrojNaStanju = x.Knjige.BrojNaStanju,
            //    BrojIzdatih = x.Knjige.BrojIzdatih,
            //})
            //.GroupJoin(_context.IzdavackeKuce,
            //           izdanje => izdanje.IzdavackaKucaId,
            //           izdavackaKuca => izdavackaKuca.Id,
            //           (izdanje, izdavackeKuceGroup) => new { Izdanje = izdanje, IzdavackeKuceGroup = izdavackeKuceGroup })
            //.SelectMany(x => x.IzdavackeKuceGroup.DefaultIfEmpty(),
            //            (x, izdavackaKuca) => new { Izdanje = x.Izdanje, IzdavackaKuca = izdavackaKuca })
            //.GroupJoin(_context.Knjige,
            //           x => x.Izdanje.KnjigaId,
            //           knjiga => knjiga.Id,
            //           (x, knjigeGroup) => new { Izdanje = x.Izdanje, IzdavackaKuca = x.IzdavackaKuca, KnjigeGroup = knjigeGroup })
            //.SelectMany(x => x.KnjigeGroup.DefaultIfEmpty(),
            //            (x, knjiga) => new NajnovijaIzdanjaDTO
            //            {
            //                IzdanjeID = x.Izdanje.Id,
            //                Godina = x.Izdanje.Godina,
            //                IzdavackaKuca = x.IzdavackaKuca != null ? x.IzdavackaKuca.Naziv : null,
            //                Knjiga = knjiga != null ? knjiga.Naslov : null,
            //                SlikaKorica = x.Izdanje.SlikaKorica,
            //                BrojNaStanju = knjiga != null ? knjiga.BrojNaStanju : 0,
            //                BrojIzdatih = knjiga != null ? knjiga.BrojIzdatih : 0,
            //            })
            //.ToPagedList(pageNumber, pageSize);


            return View(izdanja);
        }

    }
}