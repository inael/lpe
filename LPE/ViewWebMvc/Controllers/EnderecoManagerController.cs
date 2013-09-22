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
    public class EnderecoManagerController : Controller
    {
        EnderecoBll negocio;

        public EnderecoManagerController()
        {
            negocio = new EnderecoBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<Endereco> listaEndereco; 
            try
            {
                listaEndereco = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição de endereços.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaEndereco.Select(e => new { e.IdEndereco, e.Logradouro, e.Bairro, e.Cep }));
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
                Endereco entity = new Endereco
                {
                    IdMunicipioEndereco = new Municipio { IdMunicipio = Convert.ToInt32(collection["selectMunicipio[]"]) },
                    Logradouro = collection["Logradouro"],
                    Bairro = collection["Bairro"],
                    Cep = collection["Cep"]
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
                Endereco entity = new Endereco
                {
                    IdEndereco = Convert.ToInt32(collection["id"]),
                    IdMunicipioEndereco = new Municipio { IdMunicipio = Convert.ToInt32(collection["selectMunicipio[]"]) },
                    Logradouro = collection["Logradouro"],
                    Bairro = collection["Bairro"],
                    Cep = collection["Cep"]
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
            Endereco entity;

            int idEndereco = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idEndereco);
            }
            
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do endereço.";
                throw e;
            }

            AddressViewModel Address = new AddressViewModel
            {
                idEndereco = entity.IdEndereco,
                idMunicipio = entity.IdMunicipioEndereco.IdMunicipio.ToString(),
                selectMunicipio = entity.IdMunicipioEndereco.NomeMunicipio,
                Logradouro = entity.Logradouro,
                Bairro = entity.Bairro,
                Cep = entity.Cep
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
                Endereco entidade = negocio.Consultar(id);
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
            Endereco entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
