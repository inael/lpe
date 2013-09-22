using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Negocio;
using Modelo;
using Core.Serialization;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace ViewWebMvc.Controllers
{
    public class ResultadoManagerController : PDFController
    {
        ResultadoBll negocio;

        public ResultadoManagerController()
        {
            negocio = new ResultadoBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public string List()
        {
            List<Resultado> listaResultado;
            try
            {
                listaResultado = negocio.ListarAtivos();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição de endereços.";
                throw e;
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(listaResultado.Select(p => new {
                                                                            p.IdResultado,
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
        [Authorize(Roles = "Admin")]
        public ActionResult ViewResult()
        {
            return PartialView();
        }

        [HttpPost]
        public string Create(FormCollection collection)
        {
            try
            {
                //validateParameterList(ProductForm);
                //Resultado entity = new Resultado
                //{
                //    NomeResultado = collection["NomeResultado"],
                //    SobrenomeResultado = collection["SobrenomeResultado"],
                //    SexoResultado = Convert.ToChar(collection["SexoResultado"]),
                //    TratamentoResultado = "Sr(a).",
                //    IdEstadoCivil = new EstadoCivil { IdEstadoCivil = Convert.ToInt32(collection["EstadoCivil[]"]) },
                //    CPFResultado = collection["CPFResultado"],
                //    EmailResultado = collection["EmailResultado"],
                //    IdadeResultado = Convert.ToInt32(collection["IdadeResultado"]),
                //    DtNascimentoResultado = Convert.ToDateTime(collection["Ano"] + "/" + collection["Mes"] + "/" + collection["Dia"]),
                //    idEndereco = new Endereco { IdEndereco = Convert.ToInt32(collection["Endereco[]"]) },
                //    IdEscolaridade = new Escolaridade { IdEscolaridade = Convert.ToInt32(collection["Escolaridade[]"]) },
                //    IdProfissao = new CBOSinonimos { IdCBOSinonimo = Convert.ToInt32(collection["Profissao[]"]) }
                //};

                //negocio.Inserir(entity);
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

                //Resultado entity = new Resultado
                //{
                //    IdResultado = Convert.ToInt32(collection["id"]),
                //    NomeResultado = collection["NomeResultado"],
                //    SobrenomeResultado = collection["SobrenomeResultado"],
                //    SexoResultado = Convert.ToChar(collection["SexoResultado"]),
                //    TratamentoResultado = "Sr(a).",
                //    IdEstadoCivil = new EstadoCivil { IdEstadoCivil = Convert.ToInt32(collection["EstadoCivil[]"]) },
                //    CPFResultado = collection["CPFResultado"],
                //    EmailResultado = collection["EmailResultado"],
                //    IdadeResultado = Convert.ToInt32(collection["IdadeResultado"]),
                //    DtNascimentoResultado = Convert.ToDateTime(collection["Ano"] + "/" + collection["Mes"] + "/" + collection["Dia"]),
                //    idEndereco = new Endereco { IdEndereco = Convert.ToInt32(collection["Endereco[]"]) },
                //    IdEscolaridade = new Escolaridade { IdEscolaridade = Convert.ToInt32(collection["Escolaridade[]"]) },
                //    IdProfissao = new CBOSinonimos { IdCBOSinonimo = Convert.ToInt32(collection["Profissao[]"]) }
                //};

                //negocio.Alterar(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na alteraçao do endereço.";
                throw e;
            }

            return "True";
        }

        //[HttpPost]
        public ActionResult UpdateSetPdf(int id)
        {
            
            try
            {
                //validateParameterList(ProductForm);

                Resultado entity = negocio.Consultar(id);
                entity.Pdf = true;

                negocio.Alterar(entity);

                JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
                //return serializer.Serialize(entity);
                //return new RazorPDF.PdfResult(entity, "ViewResult");
                return ViewPdf(entity);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na alteraçao do endereço.";
                throw e;
            }
        }

        public void GenderPdf()
        {
            Document document = new Document(PageSize.A4, 80, 50, 30, 65);
            try
            {
                //D:\\PdfTest\\my.pdf
                string link = Server.MapPath("~");
                PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/my.pdf"), FileMode.Create));

                //document header attributes
                document.AddAuthor("betterThanZero");
                document.AddCreationDate();
                document.AddProducer();
                document.AddCreator("MySampleCode.com");
                document.AddTitle("Demo for iText XMLWorker");

                document.Open();


                //WebClient wc = new WebClient();
                //string htmlText = wc.DownloadString("http://localhost:59500/my.html");
//                string htmlText = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
//                        <HTML>
//                         <HEAD>
//                          <TITLE> LPE </TITLE>
//                          <META http-equiv='Content-Type' content='text/html; charset=UTF-8' />
//                         </HEAD>
//
//                         <BODY style='background:"+Server.MapPath("~/LPE/Content/imgs/timbrado.jpg")+@";'width:2481px; height:3505px;'>
//	                        <div id='conteudo' style='border: 1px solid black; width: 2200px; height: 2700px; margin: 600px 200px;'>
//		                        <div id='dadosPessoais'>
//			                        RELATÓRIO
//
//			                        Nome: Paulo Henrique Neto
//			                        E-mail: paulo@bigodesign.com.br
//			                        Data de Nascimento: 24/11/1987 Idade: 25 anos Sexo: M
//			                        RG: 4479524 DGPC/GO CPF: 030.301.741-40 Escolaridade: Superior Completo
//			                        Profissão: Designer Gráfico Cidade onde reside: Ap. de Goiânia/GO Data da Avaliação: 05/08/13		
//		                        </div>
//		                        <div style='font-size: 24px; color: #5e3a60;'>
//			                        <div style='border: 0px solid black; background-image:url("+Server.MapPath("~/LPE/Content/imgs/bolaVerde.jpg")+@"; width: 19px; height: 19px; margin: 5px;'></div><p>Perfil</p>
//		                        </div>
//		                        <div id='PerfilPdf'></div>
//
//		                        <div class='formTitle'>
//			                        <div class='iconCirculo'></div><p>Caracteristica</p>
//		                        </div>
//		                        <div id='CaracteristicaPdf'></div>
//
//		                        <div class='formTitle'>
//			                        <div class='iconCirculo'></div><p>Motiva</p>
//		                        </div>
//		                        <div id='MotivaPdf'></div>
//
//		                        <div class='formTitle'>
//			                        <div class='iconCirculo'></div><p>Desagrada</p>
//		                        </div>
//		                        <div id='DesagradaPdf'></div>
//
//		                        <div class='formTitle'>
//			                        <div class='iconCirculo'></div><p>Potencial</p>
//		                        </div>
//		                        <div id='PotencialPdf'></div>
//	                        </div>
//                         </BODY>
//                        </HTML>";
//                //Response.Write(htmlText);
//                List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StringReader(htmlText), null);
//                for (int k = 0; k < htmlarraylist.Count; k++)
//                {
//                    document.Add((IElement)htmlarraylist[k]);
//                }
                //XMLWorkerHelper.getInstance().ParseXHtml(writer, document, new FileInputStream ("/ html / loremipsum.html"));

                document.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public void GenderPdfXml()
        //{
        //     string strHtml = null;
        //     MemoryStream memStream = new MemoryStream();
        //     StringWriter strWriter = new StringWriter();
        //     Server.Execute("relatorio.aspx", strWriter);
        //     strHtml = strWriter.ToString();
        //     strWriter.Close();
        //     strWriter.Dispose();
        //     iTextSharp.text.Image addLogo = default(iTextSharp.text.Image);
        //     addLogo = iTextSharp.text.Image.GetInstance(Server.MapPath("imgs") + "/timbrado.jpg");
        //     string strFileShortName = "test" + DateTime.Now.Ticks + ".pdf";
        //     string strFileName = Server.MapPath("~PDF\\" + strFileShortName);
        //     iTextSharp.text.Document docWorkingDocument = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 1, 1, 0, 0);
        //     StringReader srdDocToString = null;
        //     try
        //     {
        //         PdfWriter pdfWrite = default(PdfWriter);
        //         pdfWrite = PdfWriter.GetInstance(docWorkingDocument, new FileStream(strFileName, FileMode.Create));
        //         srdDocToString = new StringReader(strHtml);
        //         docWorkingDocument.Open();
        //         addLogo.ScaleToFit(128, 37);
        //         addLogo.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //         docWorkingDocument.Add(addLogo);
        //         XMLWorkerHelper.GetInstance().ParseXHtml(pdfWrite, docWorkingDocument, srdDocToString);
        //     }
        //     catch (Exception e)
        //     {
        //         throw e;
        //     }
        //}  

        public string Search(string id)
        {
            Resultado entity;

            int idResultado = Convert.ToInt32(id);
            try
            {
                entity = negocio.Consultar(idResultado);
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na requisição do endereço.";
                throw e;
            }

            Resultado newEntity = new Resultado
            {
                IdResultado = entity.IdResultado,
                IdUsuario = entity.IdUsuario,
                IdQuestionario = entity.IdQuestionario,
                IdRelatorio = entity.IdRelatorio,
                Valor = entity.Valor,
                Pdf = entity.Pdf
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
                Resultado entidade = negocio.Consultar(id);
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
            Resultado entidade = negocio.Consultar(id);
            return View(entidade);
        }
    }
}
