using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UFC;
using UFC.Models;

namespace UFC.Controllers
{   
    public class COMPROVANTEController : Controller
    {
		private readonly ICOMPROVANTERepository comprovanteRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public COMPROVANTEController() : this(new COMPROVANTERepository())
        {
        }

        public COMPROVANTEController(ICOMPROVANTERepository comprovanteRepository)
        {
			this.comprovanteRepository = comprovanteRepository;
        }

        //
        // GET: /COMPROVANTE/

        public ViewResult Index()
        {
            return View(comprovanteRepository.AllIncluding(comprovante => comprovante.APOSTA));
        }

        //
        // GET: /COMPROVANTE/Details/5

        public ViewResult Details(int id)
        {
            return View(comprovanteRepository.Find(id));
        }

        //
        // GET: /COMPROVANTE/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /COMPROVANTE/Create

        [HttpPost]
        public ActionResult Create(COMPROVANTE comprovante)
        {
            if (ModelState.IsValid) {
                comprovanteRepository.InsertOrUpdate(comprovante);
                comprovanteRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /COMPROVANTE/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(comprovanteRepository.Find(id));
        }

        //
        // POST: /COMPROVANTE/Edit/5

        [HttpPost]
        public ActionResult Edit(COMPROVANTE comprovante)
        {
            if (ModelState.IsValid) {
                comprovanteRepository.InsertOrUpdate(comprovante);
                comprovanteRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /COMPROVANTE/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(comprovanteRepository.Find(id));
        }

        //
        // POST: /COMPROVANTE/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            comprovanteRepository.Delete(id);
            comprovanteRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

