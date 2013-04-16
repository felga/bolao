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
    public class EVENTOController : Controller
    {
		private readonly IEVENTORepository eventoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public EVENTOController() : this(new EVENTORepository())
        {
        }

        public EVENTOController(IEVENTORepository eventoRepository)
        {
			this.eventoRepository = eventoRepository;
        }

        //
        // GET: /EVENTO/

        public ViewResult Index()
        {
            return View(eventoRepository.AllIncluding(evento => evento.LUTA));
        }

        //
        // GET: /EVENTO/Details/5

        public ViewResult Details(int id)
        {
            return View(eventoRepository.Find(id));
        }

        //
        // GET: /EVENTO/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /EVENTO/Create

        [HttpPost]
        public ActionResult Create(EVENTO evento)
        {
            if (ModelState.IsValid) {
                eventoRepository.InsertOrUpdate(evento);
                eventoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /EVENTO/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(eventoRepository.Find(id));
        }

        //
        // POST: /EVENTO/Edit/5

        [HttpPost]
        public ActionResult Edit(EVENTO evento)
        {
            if (ModelState.IsValid) {
                eventoRepository.InsertOrUpdate(evento);
                eventoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /EVENTO/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(eventoRepository.Find(id));
        }

        //
        // POST: /EVENTO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            eventoRepository.Delete(id);
            eventoRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

