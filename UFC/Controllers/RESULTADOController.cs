using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UFC;
using UFC.Models;

namespace UFC.Controllers
{   
    public class RESULTADOController : Controller
    {
		private readonly IRESULTADORepository resultadoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RESULTADOController() : this(new RESULTADORepository())
        {
        }

        public RESULTADOController(IRESULTADORepository resultadoRepository)
        {
			this.resultadoRepository = resultadoRepository;
        }

        //
        // GET: /RESULTADO/

        public ViewResult Index()
        {
            return View(resultadoRepository.All);
        }

        //
        // GET: /RESULTADO/Details/5

        public ViewResult Details(int id)
        {
            return View(resultadoRepository.Find(id));
        }

        //
        // GET: /RESULTADO/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /RESULTADO/Create

        [HttpPost]
        public ActionResult Create(RESULTADO resultado)
        {
            if (ModelState.IsValid) {
                resultadoRepository.InsertOrUpdate(resultado);
                resultadoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /RESULTADO/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(resultadoRepository.Find(id));
        }

        //
        // POST: /RESULTADO/Edit/5

        [HttpPost]
        public ActionResult Edit(RESULTADO resultado)
        {
            if (ModelState.IsValid) {
                resultadoRepository.InsertOrUpdate(resultado);
                resultadoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /RESULTADO/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(resultadoRepository.Find(id));
        }

        //
        // POST: /RESULTADO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            resultadoRepository.Delete(id);
            resultadoRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

