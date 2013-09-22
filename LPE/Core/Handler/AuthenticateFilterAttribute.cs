using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using System.Web.Routing;

namespace Core.Handler
{
    public class AuthenticateFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String user = (Usuario)System.Web.HttpContext.Current.Session["user"] != null ? ((dynamic)System.Web.HttpContext.Current.Session["user"]).Login : null;
            String action = filterContext.ActionDescriptor.ActionName;

            String sessionId = System.Web.HttpContext.Current.Session.SessionID;
            string cacheId = user!=null ? Convert.ToString(System.Web.HttpContext.Current.Cache[user]) : null;
            //string cacheId = user != null ? sessionId : null;
                      
            if (user == null || (user != null && (sessionId != cacheId)))
            {
                if (action == "OptionMenu")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "LogOn" } });
                }
                else
                {
                    /*filterContext.Controller.TempData["Message"] = "Sem acesso a esta página. Faça o login. Usuario:"+user+"/sessionId:"+sessionId+"/chacheId"+cacheId;
                    filterContext.Controller.TempData["MessageStatus"] = "401";
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Overview" } });*/
                    throw new Exception("Sem acesso a esta página. A sessão expirou ou o usuário se conectou em outro navegador/computador.<br/> Faça o login novamente.");
                }
            }         
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}