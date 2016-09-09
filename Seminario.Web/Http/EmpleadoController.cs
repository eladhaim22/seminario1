using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Seminario.Model;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;

namespace Seminario.Web.Http
{
	public class EmpleadoController : ApiController
	{
		public IEmpleadoService EmpleadoService;

		public EmpleadoController(IEmpleadoService empleadoService)
		{
			this.EmpleadoService = empleadoService;
		}

		[HttpGet]
		public HttpResponseMessage GetAllEmpleados()
		{
			var empleados = new List<EmpleadoDto>();
			empleados = EmpleadoService.GetAll().ToList();
			var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, empleados);
			return response;
		}
	}
}
