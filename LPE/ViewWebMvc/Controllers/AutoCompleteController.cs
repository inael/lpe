using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using Core.Serialization;
using Core.Handler;
using Modelo;
using AutoCompleteModel;
using Persistencia;
using System.Collections;

namespace ViewWebMvc.Controllers
{
    [AuthenticateFilter]
    [ErrorHandler]
    public class AutoCompleteController : Controller
    {
        //
        // GET: /SearchPlugin/AutoComplete/

        public AutoCompleteController()
        {

        }

        public String Index(String classe, String assembly, String tag, String table, String key, String value)
        {
            object result = null;

            try
            {
                Type Generic = typeof(Persistencia.AutoCompleteResultDao<>);
                Type ClassEntity = Type.GetType(classe + "," + assembly);
                Type Constructed = Generic.MakeGenericType(new Type[] { ClassEntity });
                
                if (ClassEntity != null)
                {
                    MethodInfo _method = Constructed.GetMethod("GetAutoCompleteList", new Type[] { typeof(String) });
                    MethodInfo genericMethod = _method.MakeGenericMethod(new Type[] { ClassEntity });
                    if (_method != null)
                    {
                        ParameterInfo[] parameters = _method.GetParameters();
                        
                        object classInstance = Activator.CreateInstance(Constructed);

                        if (parameters.Length == 0)
                        {
                            result = _method.Invoke(classInstance, null);
                        }
                        else
                        {
                            string query = String.Format(@"SELECT {1}, {0}
                                                             FROM {2} WHERE {1} LIKE '%{3}%'", value, key, table, tag);
                            object[] parametersArray = new object[] { query };
                            result = genericMethod.Invoke(classInstance, parametersArray);
                        }
                    }
                }
                else
                {
                    throw new Exception("Classe não encontrada: " + classe);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro durante o processamento da requisição.\n Message:" + e.Message + "Inner Exception:" + e.InnerException);
            }

            JavaScriptSerializer serializer = JsDateTimeSerializer.GetSerializer();
            return serializer.Serialize(result);
        }
    }
}
