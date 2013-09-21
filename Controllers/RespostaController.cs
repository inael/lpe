using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Negocio;
using ViewWebMvc.Models;
using Core.Handler;
using Core.Serialization;

namespace ViewWebMvc.Controllers
{
    [ErrorHandler]
    //[AuthenticateFilter]
    public class RespostaController : Controller
    {
        //
        // GET: /FormResposta/
        RespostaBll bllResposta;
        QuestionarioBll bllQuestionario;
        QuestaoToQuestionarioBll bllQuestaoToQuestionario;
        OpcaoRespostaToQuestionarioBll bllOpcaoRespostaToQuestionario;
        UsuarioToQuestionarioBll bllUsuarioQuestionario;
        UsuarioBll bllUsuario;

        public RespostaController()
        {
            bllResposta = new RespostaBll();
            bllQuestionario = new QuestionarioBll();
            bllQuestaoToQuestionario = new QuestaoToQuestionarioBll();
            bllOpcaoRespostaToQuestionario = new OpcaoRespostaToQuestionarioBll();
            bllUsuarioQuestionario = new UsuarioToQuestionarioBll();
            bllUsuario = new UsuarioBll();
        }

        public ActionResult FormResposta(int idQuest, int idUser)
        {
            Usuario entityUser = bllUsuario.Consultar(idUser);
            Questionario entityQuestionario = bllQuestionario.Consultar(idQuest);
            List<QuestaoToQuestionario> listQuestoes = bllQuestaoToQuestionario.ListarQuestaoToQuestionario(idQuest).ToList();
            List<OpcaoRespostaToQuestionario> listOpcoesResposta = bllOpcaoRespostaToQuestionario.ListarOpcaoRespostaToQuestionario(idQuest).ToList();
            List<UsuarioToQuestionario> ListQuestNotComplete = bllUsuarioQuestionario.isNotQuestComplete();

            ViewData["User"] = entityUser;
            ViewData["Questionario"] = entityQuestionario;
            ViewData["Questoes"] = listQuestoes;
            ViewData["OpcoesResposta"] = listOpcoesResposta;
            ViewData["UsuarioToQuestionario"] = ListQuestNotComplete; 

            return PartialView();
        }

        public bool Inserir(int Questao, int Resposta, int Questionario, int UsuarioQuestionario, int Concluido) 
        {
            Usuario idUsuario = (Usuario)Session["user"];
            try
            {

                OpcaoRespostaToQuestionario OpcaoResposta = new OpcaoRespostaToQuestionario();
                OpcaoResposta = bllOpcaoRespostaToQuestionario.Consultar(Resposta);

                QuestaoToQuestionario Questoes = new QuestaoToQuestionario();
                Questoes = bllQuestaoToQuestionario.Consultar(Questao);

                Resposta entidade = new Resposta
                {
                    idUsuario = idUsuario,
                    idQuestaoToQuestionario = Questoes,
                    IdOpcaoRespostaQuestionario = OpcaoResposta
                };

                bllResposta.Inserir(entidade);

                if (Concluido == 1)
                { 
                    UsuarioToQuestionarioBll bllUsuarioToQuestionario = new UsuarioToQuestionarioBll();

                    QuestionarioBll bllQuestionario = new QuestionarioBll();
                    Questionario entityQuestionario = this.bllQuestionario.Consultar(Questionario);

                    UsuarioToQuestionario entidadeUsuarioQuestionario = new UsuarioToQuestionario 
                    { 
                        IdUsuarioQuestionario = UsuarioQuestionario,
                        idUsuario = idUsuario,
                        idQuestionario = entityQuestionario,
                        Concluido = true
                    };

                    bllUsuarioToQuestionario.Alterar(entidadeUsuarioQuestionario);
                
                }

                return true;
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Ocorreu um erro durante o processamento da requisição.\n USUARIO: " + idUsuario + " Message:" + e.Message + "Inner Exception:" + e.InnerException + "Source:" + e.Source + "Data:" + e.Data);
                //return false;
            }
        }

        public string GerarGrafico(int idQuest, int idUser, bool insertResult)
        {
            try
            {
                GraficoRespostaBll bllGrafico = new GraficoRespostaBll();

                string sql = @"select qg.ID_GRUPO, qg.NOME Perfil,
	                            Round((SUM(opc.valor)/((select MAX(op.valor) from OPCOES_RESPOSTA op
	                            inner join OPCOES_RESPOSTA_QUESTIONARIOS opq on op.ID_OPCAO_RESPOSTA = opq.ID_OPCAO_RESPOSTA
		                        where opq.ID_QUESTIONARIO = 2
		                        group by opq.ID_QUESTIONARIO) * count(q.ID_QUESTAO)) * 100),2) Respostas
                        from RESPOSTAS r
                    inner join QUESTOES_QUESTIONARIOS qq on r.ID_QUESTAO_QUESTIONARIO = qq.ID_QUESTAO_QUESTIONARIO
                    inner join OPCOES_RESPOSTA_QUESTIONARIOS orq on r.ID_OPCAO_RESPOSTA_QUESTIONARIO = orq.ID_OPCAO_RESPOSTA_QUESTIONARIO
                    inner join OPCOES_RESPOSTA opc on opc.ID_OPCAO_RESPOSTA = orq.ID_OPCAO_RESPOSTA
                    inner join QUESTOES q on q.ID_QUESTAO  = qq.ID_QUESTAO
                    inner join QUESTOES_GRUPOS qg on qg.ID_GRUPO = q.ID_GRUPO
                    where qq.ID_QUESTIONARIO = {0}
                        and r.ID_USUARIO = {1}
                    group by qg.ID_GRUPO, qg.NOME
                    order by qg.NOME";

                string sqlFormat = String.Format(sql, String.Join(",", idQuest), String.Join(",", idUser));

                IList<GraficoResposta> dados = bllGrafico.getChart(sqlFormat);

                if (insertResult)
                {

                    Dictionary<int, double> dictResposta = dados.ToDictionary(a => a.IdGrafico, a => a.ValorRespostas);

                    List<GraficoResposta> listResposta = dictResposta.Where(a => a.Value >= 80)
                                                                     .Select(a => new GraficoResposta { IdGrafico = a.Key, ValorRespostas = a.Value })
                                                                     .ToList();

                    InserirResultado(idUser, idQuest, listResposta);
                }

                JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
                string json = serializer.Serialize(dados);
                return json;
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na geração do Gráfico.";
                throw e;
            }
        }

        public bool InserirResultado(int User, int Quest, List<GraficoResposta> Respostas)
        {
            try
            {
                RelatorioBll bllRelatorio = new RelatorioBll();
                ResultadoBll bllResultado = new ResultadoBll();
                
                string idGrupo = "";
                Usuario idUsuario = bllUsuario.Consultar(User);
                Resultado entidadeResultado;
                Questionario idQuestionario = new Questionario { IdQuestionario = Quest };

                for (int i = 0; i <= Respostas.Count - 1; i++)
                {
                    idGrupo = idGrupo + Respostas[i].IdGrafico.ToString();
                }

                List<Relatorio> listRelatorio = bllRelatorio.ListarRelatorioPorValor(Respostas[0].ValorRespostas, Convert.ToInt32(idGrupo));

                entidadeResultado = new Resultado
                {
                    IdUsuario = idUsuario,
                    IdQuestionario = idQuestionario,
                    IdRelatorio = listRelatorio[0],
                    Valor = Respostas[0].ValorRespostas,
                    UsuarioInclusao = idUsuario.Pessoa_Usuario.NomePessoa
                };
                
                bllResultado.Inserir(entidadeResultado);

                return true;
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na gravação dos dados do resultado.";
                throw e;
            }
        }
    }
}