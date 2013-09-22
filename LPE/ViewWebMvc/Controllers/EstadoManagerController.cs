using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Modelo;
using Core.Serialization;

namespace ViewWebMvc.Controllers
{
    public class EstadoManagerController : Controller
    {
        EstadoBll negocio;

        public EstadoManagerController()
        {
            negocio = new EstadoBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<Estado> listaEstado;
            try
            {
                listaEstado = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição de ESTADOS.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaEstado.Select(e => new { e.IdEstado, e.UF, e.NomeEstado }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateAndUpdate()
        {
            return PartialView();
        }

        [HttpPost]
        public string Create(FormCollection collection)
        {
            try
            {
                //validateParameterList(ProductForm);
                Estado entity = new Estado
                {
                    UF = collection["UF"],
                    NomeEstado = collection["NomeEstado"]
                };

                negocio.Inserir(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro no cadastro de ESTADOS.";
                throw e;
            }

            return "True";
        }

        [HttpPost]
        public string Update(FormCollection collection)
        {
            try
            {
                //validateParameterList(ProductForm);
                Estado entity = new Estado
                {
                    IdEstado = Convert.ToInt32(collection["id"]),
                    UF = collection["UF"],
                    NomeEstado = collection["NomeEstado"]
                };

                negocio.Alterar(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na alteraçao do ESTADO.";
                throw e;
            }

            return "True";
        }

        public string Search(string id)
        {
            Estado entity;

            int idEstado = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idEstado);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do ESTADO.";
                throw e;
            }

            Estado newEntity = new Estado
            {
                IdEstado = entity.IdEstado,
                UF = entity.UF,
                NomeEstado = entity.NomeEstado
            };

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            string json = serializer.Serialize(newEntity);
            return json;
        }

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                Estado entidade = negocio.Consultar(id);
                negocio.ExcluirLogico(entidade);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na remoção do ESTADO.";
                throw e;
            }
            return true;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Estado entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
