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
    public class MunicipioManagerController : Controller
    {
        MunicipioBll negocio;

        public MunicipioManagerController()
        {
            negocio = new MunicipioBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<Municipio> listaMunicipio;
            try
            {
                listaMunicipio = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição de endereços.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaMunicipio.Select(e => new { e.IdMunicipio, e.IdEstadoMunicipio.UF, e.NomeMunicipio, e.IDUF }));
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
                Municipio entity = new Municipio
                {
                    NomeMunicipio = collection["NomeMunicipio"],
                    //IdEstado = collection["IdEstado"],
                    IBGE = collection["IBGE"],
                    IDUF = Convert.ToInt32(collection["IDUF"])
                };

                negocio.Inserir(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro no cadastro do endereço.";
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
                Municipio entity = new Municipio
                {
                    IdMunicipio = Convert.ToInt32(collection["id"]),
                    NomeMunicipio = collection["NomeMunicipio"],
                    //IdEstado = collection["IdEstado"],
                    IBGE = collection["IBGE"],
                    IDUF = Convert.ToInt32(collection["IDUF"])
                };

                negocio.Alterar(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na alteraçao do endereço.";
                throw e;
            }

            return "True";
        }

        public string Search(string id)
        {
            Municipio entity;

            int idMunicipio = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idMunicipio);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do endereço.";
                throw e;
            }

            Municipio newEntity = new Municipio
            {
                IdMunicipio = entity.IdMunicipio,
                NomeMunicipio = entity.NomeMunicipio,
                IdEstadoMunicipio = entity.IdEstadoMunicipio,
                IBGE = entity.IBGE,
                IDUF = entity.IDUF
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
                Municipio entidade = negocio.Consultar(id);
                negocio.ExcluirLogico(entidade);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na remoção do Endereço.";
                throw e;
            }
            return true;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Municipio entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
