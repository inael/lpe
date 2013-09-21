using System;
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
    public class ReportManagerController : Controller
    {
        //
        // GET: /ReportManager/
        public ActionResult Index()
        {
            return View();
        }

    }
}