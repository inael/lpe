using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Modelo;

namespace ViewWebMvc.Controllers
{
    public class UsuarioQuestionarioController : Controller
    {
        UsuarioToQuestionarioBll negocio;

        public UsuarioQuestionarioController()
        {
            negocio = new UsuarioToQuestionarioBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<UsuarioToQuestionario> lista = negocio.Listar();
            return View(lista);
        }

        public ActionResult IndexSelect()
        {
            List<UsuarioToQuestionario> lista = negocio.ListarAtivos();
            return View(lista);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            UsuarioToQuestionario entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection, UsuarioToQuestionario entidade)
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
            UsuarioToQuestionario entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection, UsuarioToQuestionario entidade)
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
            UsuarioToQuestionario entidade = negocio.Consultar(id);
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
