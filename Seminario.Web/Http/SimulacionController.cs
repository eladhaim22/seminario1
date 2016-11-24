using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Seminario.Ioc.Contracts;
using Seminario.Model;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;

namespace Seminario.Web.Http
{
	public class SimulacionController : ApiController
	{
		private readonly float BancoCentralLimit = 300000;

		public ISimulacionService SimulacionService { get; set; }

		public IProductoService ProductoService { get; set; }

		public SimulacionController(ISimulacionService simulacionService, IProductoService productoService)
		{
			this.SimulacionService = simulacionService;
			this.ProductoService = productoService;
		}

		[HttpPost]
		public HttpResponseMessage CreateSimulacion(SimulacionDto data)
		{
			if (isBancoCentralProduct(data) && data.ValorNominal != null )
			{
				var bancoCentralCreditLeft = checkForLimiteBancoCentral(data.FechaDescuento);
                //sacado por agustin, esta valicación no es necesaria, se muestran cantidades pero no se valida nada.
				//if (bancoCentralCreditLeft - data.ValorNominal < 0)
				//{
				//	string msg = "La opreacion del producto de banco central que se puede realizar son de " + bancoCentralCreditLeft;
				//	return ControllerContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new ServiceException(msg));
				//}
			}
			SimulacionService.Create(data);
			var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
			return response;
		}

		[HttpPost]
		[Authorize(Roles = "Jefe")]
		public HttpResponseMessage UpadateSimulacion(SimulacionToUpdate simulacionToUpdate)
		{
			if (simulacionToUpdate.State == 0)
			{
				if (isBancoCentralProduct(simulacionToUpdate.Simulacion))
				{
					var bancoCentralCreditLeft = checkForLimiteBancoCentral(simulacionToUpdate.Simulacion.FechaDescuento);
					if (bancoCentralCreditLeft - simulacionToUpdate.Simulacion.ValorNominal < 0)
					{
						string msg = "La opreacion del producto de banco central que se puede realizar son de" + bancoCentralCreditLeft;
						return ControllerContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new ServiceException(msg));
					}
				}
				simulacionToUpdate.Simulacion.Estado = TipoEstadoDto.Aceptado;
			}
			else
				simulacionToUpdate.Simulacion.Estado = TipoEstadoDto.Rechazado;

			SimulacionService.Update(simulacionToUpdate.Simulacion);
			var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}

        
		[HttpPost]
        [Authorize(Roles = "Oficial")]
        public HttpResponseMessage ConfirmarSimulacion(SimulacionDto simulacion)
        {
            if (isBancoCentralProduct(simulacion))
            {
                var bancoCentralCreditLeft = checkForLimiteBancoCentral(simulacion.FechaDescuento);
                if (bancoCentralCreditLeft - simulacion.ValorNominal < 0)
                {
                    string msg = "La opreación del producto de banco central que se puede realizar son de" + bancoCentralCreditLeft;
                    return ControllerContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new ServiceException(msg));
                }
            }
            simulacion.Estado = TipoEstadoDto.Confirmada;
            SimulacionService.Update(simulacion);
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
				response = ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new ServiceException("simulacion no existe"));
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

		[HttpGet]
		public HttpResponseMessage GetRemainingLip()
		{
			return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, checkForLimiteBancoCentral(DateTime.Now));
		}

		public class SimulacionToUpdate
		{
			public SimulacionDto Simulacion { get; set; }
			public int State { get; set; }
		}

		private bool isBancoCentralProduct(SimulacionDto simulacion)
		{
            try
            {
                var producto = ProductoService.GetById(simulacion.CodProd);
                if (producto.CodigoProducto == 530)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
		}

		private float checkForLimiteBancoCentral(DateTime simulacionDate)
		{
			var bancoCentralActualMonth = this.SimulacionService.GetMany(x =>
				x.Estado == TipoEstado.Aceptado && x.Producto.CodigoProducto == 530 && x.FechaDescuento.Month == simulacionDate.Month &&
				 x.FechaDescuento.Year == simulacionDate.Year).ToList();
           
			var remainingLip = BancoCentralLimit - bancoCentralActualMonth.Sum(x => x.ValorNominal);
            return remainingLip != null ? remainingLip : 0;
		}
	}
}
