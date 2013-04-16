using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UFC;
using UFC.Models;

namespace UFC.Controllers
{       
    public class APOSTAController : Controller
    {
		private readonly IAPOSTARepository apostaRepository;
        private readonly IUsuarioRepository usuarioRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public APOSTAController()
            : this(new APOSTARepository(), new UsuarioRepository())
        {
        }

        public APOSTAController(IAPOSTARepository apostaRepository, IUsuarioRepository usuarioRepository)
        {
			this.apostaRepository = apostaRepository;
            this.usuarioRepository = usuarioRepository;
        }

        //
        // GET: /APOSTA/

        public ViewResult Index()
        {
            UFC.Models.IEVENTORepository eventoRepository = new UFC.Models.EVENTORepository();
            UFC.EVENTO oEvento = eventoRepository.Find(int.Parse(Request.Params["Evento"]));
            TempData["EventoSelecionado"] = oEvento;
            return View(apostaRepository.All);
        }

        public ViewResult SelecionaEvento()
        {
            UFC.Models.IEVENTORepository eventoRepository = new UFC.Models.EVENTORepository();
            ViewBag.PossibleEvento = eventoRepository.All.Where(x => x.LIBERADO==true).OrderBy(x => x.DATA);
            return View(apostaRepository.All);
        }

        //
        // GET: /APOSTA/Details/5

        public ViewResult Details(int id)
        {
            return View(apostaRepository.Find(id));
        }

        //
        // GET: /APOSTA/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult LiberarResultados()
        {
            IEVENTORepository eventoRepository = new EVENTORepository();
            EVENTO oEvento = eventoRepository.Find(int.Parse(Request.Params["numeroevento"].ToString()));
            oEvento.RESULTADODISPONIVEL = true;
            eventoRepository.InsertOrUpdate(oEvento);
            eventoRepository.Save();
            TempData["message"] = "Resultados Liberados!";
            return RedirectToAction("SelecionaEvento");
        }

        [Authorize]
        [HttpPost]
        public ActionResult GerarResultados()
        {    
            int pontosacertovencedor = 10;
            int pontoacertoround = 5;
            int pontoacertomodo = 6;

            IEVENTORepository eventoRepository = new EVENTORepository();
            EVENTO oEvento = eventoRepository.Find(int.Parse(Request.Params["numeroevento"].ToString()));

            Dictionary<int, int> resultadolutavencedor = new Dictionary<int, int>();
            Dictionary<int, int> resultadolutaround = new Dictionary<int, int>();
            Dictionary<int, string> resultadolutamodo = new Dictionary<int, string>();

            IEnumerable<APOSTA> oApostas = apostaRepository.All.Where(x => x.LUTA.IDEVENTO == oEvento.ID && x.RESULTADO == true);
            foreach (APOSTA resultado in oApostas)
            {
                resultadolutavencedor.Add(resultado.LUTA.ID,resultado.LUTADORVENCEDOR);
                resultadolutaround.Add(resultado.LUTA.ID,resultado.ROUND);
                resultadolutamodo.Add(resultado.LUTA.ID,resultado.MODO);
            }


            IRESULTADORepository resultadoRepository = new RESULTADORepository();
            foreach (RESULTADO result in resultadoRepository.All.Where(x => x.IDEVENTO == oEvento.ID))
            {
                resultadoRepository.Delete(result.ID);
            }
            resultadoRepository.Save();
            IEnumerable<COMPROVANTE> oComprovantes = (from a in apostaRepository.All.Where(x => x.LUTA.IDEVENTO == oEvento.ID && x.RESULTADO == false) select a.COMPROVANTE).Distinct();
            foreach (COMPROVANTE comprovante in oComprovantes)
            {

                RESULTADO oResultado = new RESULTADO();
                oResultado.IDCOMPROVANTE = comprovante.ID;
                oResultado.DATAAPOSTA = comprovante.DATA;
                oResultado.NOME = comprovante.Usuario.Nome;
                oResultado.IDEVENTO = oEvento.ID;
                int pontuacao = 0;
                foreach (APOSTA aposta in comprovante.APOSTA)
	            {
                    int pontosaposta = 0;
		            int lutadorvencedor = resultadolutavencedor[aposta.LUTA.ID];
                    int roundfinal = resultadolutaround[aposta.LUTA.ID];
                    if(lutadorvencedor == aposta.LUTADORVENCEDOR)
                    {
                        pontosaposta += pontosacertovencedor;
                        if(roundfinal == aposta.ROUND)
                        {
                            pontosaposta += pontoacertoround;
                        }
                        if (resultadolutamodo[aposta.LUTA.ID] == aposta.MODO)
                        {
                            pontosaposta += pontoacertomodo;
                        }
                    }
                    aposta.PONTOS = short.Parse(pontosaposta.ToString());
                    pontuacao += pontosaposta;
            	}
               oResultado.PONTUACAO = pontuacao;
               resultadoRepository.InsertOrUpdate(oResultado);
               resultadoRepository.Save();
            }
            apostaRepository.Save();
            TempData["message"] = "Pontuação calculada com sucesso!";
            return RedirectToAction("SelecionaEvento");
        }
        //
        // POST: /APOSTA/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(APOSTA aposta)
        {
            try
            {
                IEVENTORepository eventoRepository = new EVENTORepository();
                EVENTO oEvento = eventoRepository.Find(int.Parse(Request.Params["numeroevento"].ToString()));
                Usuario oUsuario = usuarioRepository.All.Where(x => x.Login == HttpContext.User.Identity.Name).First();
                int idcomprovante = 0;
                bool resultado = false;
                if (User.IsInRole("admin"))
                {
                    if (int.Parse(Request.Params["resultado"].ToString()) == 1)
                    {
                        resultado = true;
                        IEnumerable<APOSTA> oApostas = apostaRepository.All.Where(x => x.LUTA.IDEVENTO == oEvento.ID && x.RESULTADO == true);
                        foreach (APOSTA apostar in oApostas)
                        {
                            apostaRepository.Delete(apostar.ID);
                        }
                    }
                }
                //if ((oUsuario.SALDO < oEvento.VALOR) && !resultado)
                //{
                //    TempData["message"] = "Aposta não realizada, saldo insuficiente!";
                //    return RedirectToAction("SelecionaEvento");
                //}                
                //else
                //{
                    ICOMPROVANTERepository comprovanteRepository = new COMPROVANTERepository();
                    COMPROVANTE oComprovante = new COMPROVANTE();
                    oComprovante.DATA = DateTime.Now;
                    oComprovante.IDUSUARIO = oUsuario.Id;
                    oComprovante.TIPO = "A";
                    oComprovante.VALOR = oEvento.VALOR;
                    comprovanteRepository.InsertOrUpdate(oComprovante);
                    comprovanteRepository.Save();
                    idcomprovante = oComprovante.ID;
                    //if (!resultado)
                    //{
                    //    oUsuario.SALDO = oUsuario.SALDO - oEvento.VALOR;
                    //    usuarioRepository.InsertOrUpdate(oUsuario);
                    //    usuarioRepository.Save();
                    //}
                //}
                IEnumerable<LUTA> oLutas = oEvento.LUTA;
                foreach (LUTA luta in oLutas)
                {
                    APOSTA oAposta = new APOSTA();
                    oAposta.IDLUTA = luta.ID;
                    oAposta.IDUSUARIO = oUsuario.Id;
                    oAposta.LUTADORVENCEDOR = short.Parse(Request.Form[luta.ID.ToString()]);
                    oAposta.ROUND = short.Parse(Request.Form["ROUND" + luta.ID.ToString()]);
                    oAposta.MODO = Request.Form["MODO" + luta.ID.ToString()];
                    oAposta.IDCOMPROVANTE = idcomprovante;
                    oAposta.RESULTADO = resultado;
                    apostaRepository.InsertOrUpdate(oAposta);
                    apostaRepository.Save();
                }
                TempData["message"] = "Aposta realizada com sucesso!";
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return RedirectToAction("SelecionaEvento");
        }
        
        //
        // GET: /APOSTA/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
             return View(apostaRepository.Find(id));
        }

        //
        // POST: /APOSTA/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(APOSTA aposta)
        {
            if (ModelState.IsValid) {
                apostaRepository.InsertOrUpdate(aposta);
                apostaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /APOSTA/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(apostaRepository.Find(id));
        }

        //
        // POST: /APOSTA/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            apostaRepository.Delete(id);
            apostaRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

