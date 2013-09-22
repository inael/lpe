using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Handler
{
    public class ErrorHandlerReturnData
    {
        public string FriendlyMessage {get; set; }
        public string Message { get; set; }
        public Exception InnerException { get; set; }
        public string StackTrace { get; set; }
    }

    public class ErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string Message = (string)filterContext.Controller.TempData["ErrorMessage"];
            if (Message == null)
                Message = filterContext.Exception.Message;

            if (Message == null || Message == "")
            {
                Message = "Erro desconhecido no servidor.";
            }

            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            filterContext.Result = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new ErrorHandlerReturnData
                {
                    FriendlyMessage = Message,
                    Message = filterContext.Exception.Message,
                    InnerException = filterContext.Exception.InnerException,
                    StackTrace = filterContext.Exception.StackTrace
                }
            };
            filterContext.ExceptionHandled = true;
        }
    }
}