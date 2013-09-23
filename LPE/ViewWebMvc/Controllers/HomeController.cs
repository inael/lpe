using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewWebMvc.Seguranca;
using System.Web.Security;
using System.Web.Routing;
using Negocio;
using Modelo;
using Core.Handler;
using Core.Serialization;
using System.Collections;

namespace ViewWebMvc.Controllers
{
    [ErrorHandler]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            RedirectToAction("Logout");
            
            string[] myCookies = Request.Cookies.AllKeys;

            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }

            return View();
        }

        public PartialViewResult Overview()
        {
            return PartialView();
        }

        public ActionResult TesteIntegracao()
        {
            return View();
        }

        [AuthenticateFilter]
        public ActionResult OptionMenu()
        {
            MenuOpcaoBll bllMenu = new MenuOpcaoBll();
            UsuarioToQuestionarioBll bllUsuarioQuestionario = new UsuarioToQuestionarioBll();
            UsuarioBll bllUser = new UsuarioBll();

            MenuOpcao modelMenu = new MenuOpcao();

            Usuario userSession = (Usuario)Session["user"];

            string Sql = @"SELECT M.ID_MENU_OPC, M.NOME_MEN, M.PASTA_MEN, M.CONTROLADOR_MEN, 
                                  M.ACAO_MEN, M.ICONE_MEN, M.USUARIO_INCLUSAO, M.DATA_INCLUSAO,
                                  M.USUARIO_ALTERACAO, M.DATA_ALTERACAO, M.EXCLUIDO 
                             FROM MENU_OPCOES M
                            INNER JOIN MENU_OPCOES_PERFIS OP ON M.ID_MENU_OPC = OP.ID_MENU_OPC
                            INNER JOIN PERFIS P ON P.ID_PERFIL = OP.ID_PERFIL
                            INNER JOIN USUARIOS U ON P.ID_PERFIL = U.ID_PERFIL
                            WHERE U.ID_USUARIO = " + userSession.IdUsuario + " AND M.EXCLUIDO = 0";

            IList<MenuOpcao> opcoes = bllMenu.ListarMenuUsr(Sql);

            userSession = bllUser.Consultar(userSession.IdUsuario);
            
            ViewData["User"] = userSession;
            
            //se perfil cliente entao procurar questionarios para responder ou laudos
            if (userSession.Perfil_Usuario.Nome == "Administrador")
            {
                ViewData["Opcoes"] = opcoes;
                return PartialView();
            }
            else
            {
                List<UsuarioToQuestionario> ListQuestNotComplete = bllUsuarioQuestionario.isNotQuestComplete();

                List<Questionario> ListUserQuestNotComplete = new List<Questionario>();

                ListUserQuestNotComplete = ListQuestNotComplete.Where(q => q.idUsuario.IdUsuario == userSession.IdUsuario)
                                                               .ToList()
                                                               .ConvertAll<Questionario>(q => new Questionario()
                                                               {
                                                                   IdQuestionario = q.idQuestionario.IdQuestionario,
                                                                   NomeQuestionario = q.idQuestionario.NomeQuestionario
                                                               });
                
                if (ListUserQuestNotComplete.Count > 0)
                {
                    ViewData["Questionario"] = ListUserQuestNotComplete;
                    ViewData["Opcoes"] = opcoes;
                    return PartialView();    
                }
                
                ViewData["Opcoes"] = opcoes;
                return PartialView();    
            }
        }

        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult IndexMPE()
        {
            return View();
        }

        public ActionResult LoginMPE(String Email, String PasswordHash, String Nome) {
            String jsonRetornoLogin = CheckLogin(Email, PasswordHash);

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            IDictionary<string, string> objetoRetornoLogin = (IDictionary<string, string>)serializer.Deserialize(jsonRetornoLogin, typeof(IDictionary<string, string>));
            bool isValid = Boolean.Parse(objetoRetornoLogin["isValid"]);
            
            if (isValid)
            {
                return RedirectToAction("IndexMPE");
            }
            else
            {

                //retornar msg ao MPE informando que esse usuario nao esta cadastrado no LPE
                // ViewData["msg"] = "Usuário "+userName +" não encontrado na base do LPE.";
                return View("erro");
            }
        }

        public string CheckLogin(String Login, String Password)
        {
            //Boolean isValidLogin = false;
            IDictionary<string, string> returnLogin = new Dictionary<string, string>();
            Usuario modelUser = new Usuario();
            UsuarioBll bllUser = new UsuarioBll();

            try
            {
                string sKey = Login;
                string sUser = Convert.ToString(System.Web.HttpContext.Current.Cache[sKey]);
                sUser = sUser == "" ? null : sUser;

                if (Membership.ValidateUser(Login, Password))
                {
                    FormsAuthentication.SetAuthCookie(Login, true);
                    
                    String sessionId = System.Web.HttpContext.Current.Session.SessionID;
                    int sessionTimeout = System.Web.HttpContext.Current.Session.Timeout;
                    TimeSpan SessTimeOut = new TimeSpan(0, 0, sessionTimeout, 0, 0);
                    System.Web.HttpContext.Current.Cache.Insert(sKey, sessionId, null, DateTime.MaxValue, SessTimeOut,
                       System.Web.Caching.CacheItemPriority.NotRemovable, null);
                    Session["userKey"] = Login;

                    //System.Web.HttpContext.Current.Session["user"] = Login;
                    //isValidLogin = true;
                    modelUser = (Usuario)Session["user"];
                    modelUser = bllUser.Consultar(modelUser.IdUsuario);
                    returnLogin.Add("userName", modelUser.Pessoa_Usuario.NomePessoa + " " + modelUser.Pessoa_Usuario.SobrenomePessoa);
                    returnLogin.Add("isValid","true");
}
                else
                {
                    returnLogin.Add("isValid", "false");
                    ModelState.AddModelError("", "Usuário e senha inválidos.");
                }
                

                // If we got this far, something failed, redisplay form
                // return View(model);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro na verificação do login.";
                throw e;
            }

            /*if (isValidLogin)
            {
                UserPermissionHandler.SetUserPermission();
            }*/
            //return isValidLogin;

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            string json = serializer.Serialize(returnLogin);
            return json;
        }

        public void Logout()
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session["user"];
            if (user != null)
            {
                String sKey = user.Login;
                System.Web.HttpContext.Current.Cache[sKey] = "";
            }
            System.Web.HttpContext.Current.Session["user"] = null;
            System.Web.HttpContext.Current.Session["userKey"] = null;
            Session.Clear();
            Session.Abandon();
            RedirectToAction("Index");

        }
    }
}
