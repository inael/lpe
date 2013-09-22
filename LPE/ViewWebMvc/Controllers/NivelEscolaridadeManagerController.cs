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
    public class NivelEscolaridadeManagerController : Controller
    {
        NivelEscolaridadeBll negocio;

        public NivelEscolaridadeManagerController()
        {
            negocio = new NivelEscolaridadeBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<NivelEscolaridade> listaNivelEscolaridade;
            try
            {
                listaNivelEscolaridade = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do Nivel de escolaridade.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaNivelEscolaridade.Select(e => new { e.IdNivelEscolaridade, e.DescricaoNivelEscolaridade }));
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
                NivelEscolaridade entity = new NivelEscolaridade
                {
                    DescricaoNivelEscolaridade = collection["DescricaoNivelEscolaridade"]
                };

                negocio.Inserir(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro no cadastro de Nivel de escolaridade.";
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
                NivelEscolaridade entity = new NivelEscolaridade
                {
                    IdNivelEscolaridade = Convert.ToInt32(collection["id"]),
                    DescricaoNivelEscolaridade = collection["DescricaoNivelEscolaridade"]
                };

                negocio.Alterar(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na alteraçao do Nivel de escolaridade.";
                throw e;
            }

            return "True";
        }

        public string Search(string id)
        {
            NivelEscolaridade entity;

            int idNivelEscolaridade = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idNivelEscolaridade);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do Nivel de escolaridade.";
                throw e;
            }

            NivelEscolaridade newEntity = new NivelEscolaridade
            {
                IdNivelEscolaridade = entity.IdNivelEscolaridade,
                DescricaoNivelEscolaridade = entity.DescricaoNivelEscolaridade
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
                NivelEscolaridade entidade = negocio.Consultar(id);
                negocio.ExcluirLogico(entidade);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na remoção do Nivel de escolaridade.";
                throw e;
            }
            return true;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            NivelEscolaridade entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
