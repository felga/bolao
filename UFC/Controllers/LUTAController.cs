using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UFC;
using UFC.Models;

namespace UFC.Controllers
{
    [Authorize]
    public class LUTAController : Controller
    {
		private readonly ILUTARepository lutaRepository;
        private readonly ILUTADORRepository lutadorRepository;
        private readonly IEVENTORepository eventoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LUTAController() : this(new LUTARepository(), new LUTADORRepository(), new EVENTORepository())
        {
        }

        public LUTAController(ILUTARepository lutaRepository, ILUTADORRepository lutadorRepository, IEVENTORepository eventoRepository)
        {
			this.lutaRepository = lutaRepository;
            this.lutadorRepository = lutadorRepository;
            this.eventoRepository = eventoRepository;
        }

        //
        // GET: /LUTA/

        public ViewResult Index()
        {
            return View(lutaRepository.AllIncluding(luta => luta.APOSTA));
        }

        //
        // GET: /LUTA/Details/5

        public ViewResult Details(int id)
        {
            return View(lutaRepository.Find(id));
        }

        //
        // GET: /LUTA/Create

        public ActionResult Create()
        {
            ViewBag.PossibleLutador = lutadorRepository.All;
            ViewBag.PossibleEvento = eventoRepository.All;
            return View();
        } 

        //
        // POST: /LUTA/Create

        [HttpPost]
        public ActionResult Create(LUTA luta)
        {
            ViewBag.PossibleLutador = lutadorRepository.All;
            ViewBag.PossibleEvento = eventoRepository.All;
            if (ModelState.IsValid) {
                lutaRepository.InsertOrUpdate(luta);
                lutaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /LUTA/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.PossibleLutador = lutadorRepository.All;
            ViewBag.PossibleEvento = eventoRepository.All;
             return View(lutaRepository.Find(id));
        }

        //
        // POST: /LUTA/Edit/5

        [HttpPost]
        public ActionResult Edit(LUTA luta)
        {
            ViewBag.PossibleLutador = lutadorRepository.All;
            ViewBag.PossibleEvento = eventoRepository.All;
            if (ModelState.IsValid) {
                lutaRepository.InsertOrUpdate(luta);
                lutaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /LUTA/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(lutaRepository.Find(id));
        }

        //
        // POST: /LUTA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            lutaRepository.Delete(id);
            lutaRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

