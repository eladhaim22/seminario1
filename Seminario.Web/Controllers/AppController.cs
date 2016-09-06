using LumenWorks.Framework.IO.Csv;
using Seminario.Model;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seminario.Web.Controllers
{
    public class AppController : Controller
    {

        public IProductoService ProductoService { get; set; }

        public IDatosTTService DatosTTService { get; set; }

        public AppController(IProductoService productoService, IDatosTTService datosTTService)
        {
            this.ProductoService = productoService;
            this.DatosTTService = datosTTService;
        }

        public ActionResult App()
        {
            return View();
        }
        public ActionResult AddSimulacion()
        {
            return View();
        }


        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult ViewSimulacion()
        {
            return View();
        }
    }
}
