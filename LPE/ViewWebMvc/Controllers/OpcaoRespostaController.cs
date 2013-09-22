using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Modelo;

namespace ViewWebMvc.Controllers
{
    public class OpcaoRespostaController : Controller
    {
        OpcaoRespostaBll negocio;

        public OpcaoRespostaController()
        {
            negocio = new OpcaoRespostaBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<OpcaoResposta> lista = negocio.Listar();
            return View(lista);
        }

        public ActionResult IndexSelect()
        {
            List<OpcaoResposta> lista = negocio.ListarAtivos();
            return View(lista);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            OpcaoResposta entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection, OpcaoResposta entidade)
        {
            try
            {
                /*int idRegiao = Convert.ToInt32(collection["regiao"]);
                entidade.RegiaoCidade = new Regiao { Id = idRegiao };
                entidade.UsuarioInclusao = User.Identity.Name;
                negocio.Inserir(entidade);*/
                return RedirectToAction("Index");
            }
            catch
            {
                return View(entidade);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            OpcaoResposta entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection, OpcaoResposta entidade)
        {
            try
            {
                /*int idRegiao = Convert.ToInt32(collection["regiao"]);
                entidade.RegiaoCidade = new Regiao { Id = idRegiao };
                entidade.UsuarioAteracao = entidade.UsuarioAteracao = User.Identity.Name;
                negocio.Alterar(entidade);*/
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            OpcaoResposta entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
