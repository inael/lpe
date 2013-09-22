using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cockpit.Models;
using Common;
using Common.Infrastructure;
using Ninject;
using Cockpit.Utils;
using System.Data.SqlClient;
using System.Data;

namespace Cockpit.Handler
{
    [ErrorHandler]
    public class AccessLogFilterAttribute : ActionFilterAttribute
    {
        private static CommonKernel commonk = new CommonKernel();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IEntityRepository database = (IEntityRepository)commonk.kernel.Get(typeof(IEntityRepository));
            User user = (User)System.Web.HttpContext.Current.Session["user"];

            String action = filterContext.ActionDescriptor.ActionName;
            String controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            
            if (user != null)
            {
                database.Start();
                try
                {
                    string query = @"SELECT CODI_OPC FROM OPCOES O
                    where O.ACTN_OPC = @ACTION and O.CTRL_OPC = @CONTROLLER ";

                    List<SqlParameter> Parameters = new List<SqlParameter>();
                    Parameters.Add(new SqlParameter { ParameterName = "@ACTION", SqlDbType = SqlDbType.VarChar, Value = action });
                    Parameters.Add(new SqlParameter { ParameterName = "@CONTROLLER", SqlDbType = SqlDbType.VarChar, Value = controller });

                    List<OptionMenu> opcoes = database.GetEntities<OptionMenu>(query, Parameters.ToArray());

                    if (opcoes.Count != 0)
                    {
                        DateTime date = DateTime.Now;

                        AcessLog log = new AcessLog
                        {
                            UserCode = user.Code,
                            Date = date,
                            OptionCode = opcoes.ElementAt(0).codi_opc
                        };

                        database.InsertEntity<AcessLog>(log);
                    }
                    database.Commit();
                }
                catch (Exception e)
                {
                    database.Rollback();
                    throw new CockpitException("Ocorreu um erro durante o processamento da requisição.\n Message:" + e.Message + "Inner Exception:" + e.InnerException);
                }

            }

        }
    }
}