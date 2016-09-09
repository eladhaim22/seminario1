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
	public class SimulacionController : ApiController
	{
		public ISimulacionService SimulacionService;

		public SimulacionController(ISimulacionService simulacionService)
		{
			this.SimulacionService = simulacionService;
		}

		[HttpPost]
		public HttpResponseMessage CreateSimulacion(SimulacionDto data)
		{
			data.FechaCreacion = DateTime.Now;
			data.FechaUltimaModificacion = DateTime.Now;
			SimulacionService.Create(data);
			var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
			return response;
		}

		[HttpPost]
		public HttpResponseMessage UpadateSimulacion(SimulacionToUpdate simulacionToUpdate)
		{
			if (simulacionToUpdate.State == 0 || simulacionToUpdate.State == 3)
			{
				if (User.IsInRole("Jefe"))
				{
					if (simulacionToUpdate.State == 0)
						simulacionToUpdate.Simulacion.Estado = TipoEstadoDto.Aceptado;
					else
						simulacionToUpdate.Simulacion.Estado = TipoEstadoDto.Rechazado;
				}
				else
					return ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "no tenes permisos para ejecutar esta operacion");
			}
			SimulacionService.Update(simulacionToUpdate.Simulacion);
			var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}

		[HttpGet]
		public HttpResponseMessage GetSimulacionById(string id)
		{
			HttpResponseMessage response;
			if (id == null)
			{
				throw new NullReferenceException("simulacionId");
			}
			var simulacion = SimulacionService.GetById(Convert.ToInt32(id));
			if (simulacion != null)
				response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, simulacion);
			else
				response = ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError, "simulacion no existe");
			return response;
		}

		[HttpGet]
		public HttpResponseMessage GetAllSimulacion()
		{
			var simulaciones = new List<SimulacionDto>();
			if (User.IsInRole("Jefe"))
				simulaciones = SimulacionService.GetAll().OrderBy(x => x.FechaUltimaModificacion).ToList();
			else
				simulaciones = SimulacionService.GetMany(s => s.Empleado.Legajo == User.Identity.Name).ToList();
			simulaciones = simulaciones.Select(x => { x.CantidadCheques = x.Cheques.Count; return x; }).ToList();
			var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, simulaciones);
			return response;
		}

		public class SimulacionToUpdate
		{
			public SimulacionDto Simulacion { get; set; }
			public int State { get; set; }
		}
	}
}
