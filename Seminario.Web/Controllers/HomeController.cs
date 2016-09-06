using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LumenWorks.Framework.IO.Csv;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.Web.Models;
using Seminario.Web.ViewModel;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;

namespace Seminario.Web.Controllers
{
	public class HomeController : Controller
	{
		public IProductoService ProductoService { get; set; }

		public IDatosTTService DatosTTService { get; set; }

		public HomeController(IProductoService productoService, IDatosTTService datosTTService)
		{
			this.ProductoService = productoService;
			this.DatosTTService = datosTTService;
		}

		[Authorize(Roles = "Jefe,Oficial")]
		public ActionResult Index(string id)
		{
			if (id == null)
			{
				return View(new SimulacionViewModel
				{
					Simulacion = new SimulacionDto()
				});
			}
			ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
			return View();
		}

		[Authorize(Roles = "Jefe,Oficial")]
		public ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		[Authorize(Roles = "Jefe,Oficial")]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
