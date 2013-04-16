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
    public class LUTADORController : Controller
    {
		private readonly ILUTADORRepository lutadorRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LUTADORController() : this(new LUTADORRepository())
        {
        }

        public LUTADORController(ILUTADORRepository lutadorRepository)
        {
			this.lutadorRepository = lutadorRepository;
        }

        //
        // GET: /LUTADOR/

        public ViewResult Index()
        {
            return View(lutadorRepository.AllIncluding(lutador => lutador.LUTA, lutador => lutador.LUTA1));
        }

        //
        // GET: /LUTADOR/Details/5

        public ViewResult Details(int id)
        {
            return View(lutadorRepository.Find(id));
        }

        //
        // GET: /LUTADOR/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /LUTADOR/Create

        [HttpPost]
        public ActionResult Create(LUTADOR lutador)
        {
            if (ModelState.IsValid) {
                lutadorRepository.InsertOrUpdate(lutador);
                lutadorRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /LUTADOR/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(lutadorRepository.Find(id));
        }

        //
        // POST: /LUTADOR/Edit/5

        [HttpPost]
        public ActionResult Edit(LUTADOR lutador)
        {
            if (ModelState.IsValid) {
                lutadorRepository.InsertOrUpdate(lutador);
                lutadorRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /LUTADOR/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(lutadorRepository.Find(id));
        }

        //
        // POST: /LUTADOR/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            lutadorRepository.Delete(id);
            lutadorRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

