using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Modelo;
using Core.Serialization;
using ViewWebMvc.Models;

namespace ViewWebMvc.Controllers
{
    public class EscolaridadeManagerController : Controller
    {
        EscolaridadeBll negocio;

        public EscolaridadeManagerController()
        {
            negocio = new EscolaridadeBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<Escolaridade> listaEscolaridade;
            try
            {
                listaEscolaridade = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição de endereços.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaEscolaridade.Select(e => new { e.IdEscolaridade, e.IdNivelEscolaridadeEscolaridade.DescricaoNivelEscolaridade, e.DescricaoEscolaridade }));
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
                Escolaridade entity = new Escolaridade
                {
                    IdNivelEscolaridadeEscolaridade = new NivelEscolaridade { IdNivelEscolaridade = Convert.ToInt32(collection["selectNivel[]"]) },
                    DescricaoEscolaridade = collection["DescricaoEscolaridade"]
                };

                negocio.Inserir(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro no cadastro da escolaridade.";
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
                Escolaridade entity = new Escolaridade
                {
                    IdEscolaridade = Convert.ToInt32(collection["id"]),
                    IdNivelEscolaridadeEscolaridade = new NivelEscolaridade { IdNivelEscolaridade = Convert.ToInt32(collection["selectNivel[]"]) },
                    DescricaoEscolaridade = collection["DescricaoEscolaridade"]
                };

                negocio.Alterar(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na alteraçao da escolaridade.";
                throw e;
            }

            return "True";
        }

        public string Search(string id)
        {
            Escolaridade entity;

            int idEscolaridade = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idEscolaridade);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição da escolaridade.";
                throw e;
            }

            EscolaridadeViewModel Address = new EscolaridadeViewModel
            {
                idEscolaridade = entity.IdEscolaridade,
                idNivelEscolaridade = entity.IdNivelEscolaridadeEscolaridade.IdNivelEscolaridade.ToString(),
                selectNivel = entity.IdNivelEscolaridadeEscolaridade.DescricaoNivelEscolaridade,
                DescricaoEscolaridade = entity.DescricaoEscolaridade
            };

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            string json = serializer.Serialize(Address);
            return json;
        }

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                Escolaridade entidade = negocio.Consultar(id);
                negocio.ExcluirLogico(entidade);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na remoção da escolaridade.";
                throw e;
            }
            return true;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Escolaridade entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
