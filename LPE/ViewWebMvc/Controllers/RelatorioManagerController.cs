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
    public class RelatorioManagerController : Controller
    {
        RelatorioBll negocio;
        ResultadoBll bllResultado;

        public RelatorioManagerController()
        {
            negocio = new RelatorioBll();
            bllResultado = new ResultadoBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string ListResult()
        {
            List<Resultado> listaResultado;
            try
            {
                listaResultado = bllResultado.ListarResultadosSemPdf();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição dos resultados.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaResultado.Select(p => new { p.IdResultado,
                                                                         p.IdQuestionario.NomeQuestionario, 
                                                                         p.IdUsuario.Pessoa_Usuario.NomePessoa,
                                                                         p.IdUsuario.Pessoa_Usuario.SobrenomePessoa,
                                                                         p.IdRelatorio.IdRelatorio,
                                                                         p.Valor,
                                                                         p.IdRelatorio.IdGrupo.NomeGrupo
                                                                        }));
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
                Relatorio entity = new Relatorio
                {
                    IdGrupo = new RelatorioGrupo { IdGrupo = Convert.ToInt32(collection["Grupo[]"])},
                    //Nivel 4 - Sem classificação. Alterar caso o resultado seja feito por nivel de porcentagem (Acima, Medio ou Abaixo)
                    IdNivel = 4, //new QuestaoGrupo { IdGrupo = Convert.ToInt32(collection["Grupo[]"]) },
                    Caracteristica = collection["Caracteristica"],
                    Motiva = collection["Motiva"],
                    Desagrada = collection["Desagrada"],
                    Potencial = collection["Potencial"],
                    ValorMin = Convert.ToDouble(collection["ValorMin"]),
                    ValorMax = Convert.ToDouble(collection["ValorMax"]),
                    UsuarioInclusao = Session["user"].ToString()
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

                Relatorio entity = new Relatorio
                {
                    IdRelatorio = Convert.ToInt32(collection["id"]),
                    IdGrupo = new RelatorioGrupo { IdGrupo = Convert.ToInt32(collection["GrupoRel[]"]) },
                    //Nivel 4 - Sem classificação. Alterar caso o resultado seja feito por nivel de porcentagem (Acima, Medio ou Abaixo)
                    IdNivel = 4, //new QuestaoGrupo { IdGrupo = Convert.ToInt32(collection["Grupo[]"]) },
                    Caracteristica = collection["Caracteristica"],
                    Motiva = collection["Motiva"],
                    Desagrada = collection["Desagrada"],
                    Potencial = collection["Potencial"],
                    ValorMin = Convert.ToDouble(collection["ValorMin"]),
                    ValorMax = Convert.ToDouble(collection["ValorMax"]),
                    UsuarioAteracao = Session["user"].ToString()
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
            Relatorio entity;

            int idRelatorio = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idRelatorio);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do endereço.";
                throw e;
            }

            Relatorio newEntity = new Relatorio
            {
                IdGrupo = entity.IdGrupo,
                //Nivel 4 - Sem classificação. Alterar caso o resultado seja feito por nivel de porcentagem (Acima, Medio ou Abaixo)
                IdNivel = 4, //new QuestaoGrupo { IdGrupo = Convert.ToInt32(collection["Grupo[]"]) },
                Caracteristica = entity.Caracteristica,
                Motiva = entity.Motiva,
                Desagrada = entity.Desagrada,
                Potencial = entity.Potencial,
                ValorMin = entity.ValorMin,
                ValorMax = entity.ValorMax
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
                Relatorio entidade = negocio.Consultar(id);
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
            Relatorio entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
