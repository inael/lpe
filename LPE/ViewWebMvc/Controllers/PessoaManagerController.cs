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
    public class PessoaManagerController : Controller
    {
        PessoaBll negocio;

        public PessoaManagerController()
        {
            negocio = new PessoaBll();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<Pessoa> listaPessoa;
            try
            {
                listaPessoa = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição de endereços.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaPessoa.Select(p => new { p.IdPessoa, p.TratamentoPessoa, p.NomePessoa, p.SobrenomePessoa, p.EmailPessoa }));
        }

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
                Pessoa entity = new Pessoa
                {
                    NomePessoa = collection["NomePessoa"],
                    SobrenomePessoa = collection["SobrenomePessoa"],
                    SexoPessoa = Convert.ToChar(collection["SexoPessoa"]),
                    TratamentoPessoa = "Sr(a).",
                    IdEstadoCivil = new EstadoCivil { IdEstadoCivil = Convert.ToInt32(collection["EstadoCivil[]"]) },
                    CPFPessoa = collection["CPFPessoa"],
                    EmailPessoa = collection["EmailPessoa"],
                    IdadePessoa = Convert.ToInt32(collection["IdadePessoa"]),
                    DtNascimentoPessoa = Convert.ToDateTime(collection["Ano"] + "/" + collection["Mes"] + "/" + collection["Dia"]),
                    idEndereco = new Endereco { IdEndereco = Convert.ToInt32(collection["Endereco[]"]) },
                    IdEscolaridade = new Escolaridade { IdEscolaridade = Convert.ToInt32(collection["Escolaridade[]"]) },
                    IdProfissao = new CBOSinonimos { IdCBOSinonimo = Convert.ToInt32(collection["Profissao[]"]) }
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
                
                Pessoa entity = new Pessoa
                {
                    IdPessoa = Convert.ToInt32(collection["id"]),
                    NomePessoa = collection["NomePessoa"],
                    SobrenomePessoa = collection["SobrenomePessoa"],
                    SexoPessoa = Convert.ToChar(collection["SexoPessoa"]),
                    TratamentoPessoa = "Sr(a).",
                    IdEstadoCivil = new EstadoCivil { IdEstadoCivil = Convert.ToInt32(collection["EstadoCivil[]"]) },
                    CPFPessoa = collection["CPFPessoa"],
                    EmailPessoa = collection["EmailPessoa"],
                    IdadePessoa = Convert.ToInt32(collection["IdadePessoa"]),
                    DtNascimentoPessoa = Convert.ToDateTime(collection["Ano"] + "/" + collection["Mes"] + "/" + collection["Dia"]),
                    idEndereco = new Endereco { IdEndereco = Convert.ToInt32(collection["Endereco[]"]) },
                    IdEscolaridade = new Escolaridade { IdEscolaridade = Convert.ToInt32(collection["Escolaridade[]"]) },
                    IdProfissao = new CBOSinonimos { IdCBOSinonimo = Convert.ToInt32(collection["Profissao[]"]) }
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
            Pessoa entity;

            int idPessoa = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idPessoa);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do endereço.";
                throw e;
            }

            Pessoa newEntity = new Pessoa
            {
                NomePessoa = entity.NomePessoa,
                SobrenomePessoa = entity.SobrenomePessoa,
                SexoPessoa = entity.SexoPessoa,
                TratamentoPessoa = entity.TratamentoPessoa,
                IdEstadoCivil = entity.IdEstadoCivil,
                EmailPessoa = entity.EmailPessoa,
                CPFPessoa = entity.CPFPessoa,
                IdadePessoa = entity.IdadePessoa,
                DtNascimentoPessoa = Convert.ToDateTime(entity.DtNascimentoPessoa),
                idEndereco = entity.idEndereco,
                IdEscolaridade = entity.IdEscolaridade,
                IdProfissao = entity.IdProfissao
            };

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            string json = serializer.Serialize(newEntity);
            return json;

            //return "teste";
        }

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                Pessoa entidade = negocio.Consultar(id);
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
            Pessoa entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
