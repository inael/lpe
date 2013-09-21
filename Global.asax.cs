using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.Web.Script.Serialization;
using Ninject;
using Ninject.Web.Common;
using System.Web.SessionState;

namespace ViewWebMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "AutoCompleteRoute",
               "{controller}.aspx/{classe},{assembly},{table},{key},{value}/",
               new { controller = "AutoComplete", action = "Index" }
           );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        void Session_Start(object sender, EventArgs e)
        {
            //string sessionId = Session.SessionID;
            //Session.Timeout = 2800;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        /*protected override Ninject.IKernel CreateKernel()
        {
            CommonKernel common = new CommonKernel();
            common.kernel.Load(Assembly.GetExecutingAssembly());
            return common.kernel;
        }*/

        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                // Let's write a message to show this got fired---
                // Response.Write("SessionID: " + Session.SessionID.ToString() + "User key: " + (string)Session["userKey"]);
                if (Session["userKey"] != null) // e.g. this is after an initial logon
                {
                    string sKey = (string)Session["userKey"];
                    // Accessing the Cache Item extends the Sliding Expiration automatically
                    string sUser = (string)HttpContext.Current.Cache[sKey];
                }
            }
        }
    }
}