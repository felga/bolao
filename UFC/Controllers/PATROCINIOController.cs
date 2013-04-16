using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UFC;
using UFC.Models;

namespace UFC.Controllers
{   
    public class PATROCINIOController : Controller
    {
		private readonly IPATROCINIORepository patrocinioRepository;
        private readonly IEVENTORepository eventoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PATROCINIOController():this(new PATROCINIORepository(), new EVENTORepository())
        {
        }

        public PATROCINIOController(IPATROCINIORepository patrocinioRepository, IEVENTORepository eventoRepository)
        {
			this.patrocinioRepository = patrocinioRepository;
            this.eventoRepository = eventoRepository;
        }

        //
        // GET: /PATROCINIO/

        public ViewResult Index()
        {
            return View(patrocinioRepository.All);
        }

        //
        // GET: /PATROCINIO/Details/5

        public ViewResult Details(int id)
        {
            return View(patrocinioRepository.Find(id));
        }

        //
        // GET: /PATROCINIO/Create

        public ActionResult Create()
        {
            ViewBag.PossibleEvento = eventoRepository.All;
            return View();
        } 

        //
        // POST: /PATROCINIO/Create

        [HttpPost]
        public ActionResult Create(PATROCINIO patrocinio)
        {
            ViewBag.PossibleEvento = eventoRepository.All;
            if (ModelState.IsValid) {
                patrocinioRepository.InsertOrUpdate(patrocinio);
                patrocinioRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /PATROCINIO/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.PossibleEvento = eventoRepository.All;
             return View(patrocinioRepository.Find(id));
        }

        //
        // POST: /PATROCINIO/Edit/5

        [HttpPost]
        public ActionResult Edit(PATROCINIO patrocinio)
        {
            ViewBag.PossibleEvento = eventoRepository.All;
            if (ModelState.IsValid) {
                patrocinioRepository.InsertOrUpdate(patrocinio);
                patrocinioRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /PATROCINIO/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(patrocinioRepository.Find(id));
        }

        //
        // POST: /PATROCINIO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            patrocinioRepository.Delete(id);
            patrocinioRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

