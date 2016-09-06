using Seminario.Model;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


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
            SimulacionService.Create(data);
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage UpadateSimulacion(SimulacionDto data)
        {
            SimulacionService.Update(data);
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetSimulacionById(string id)
        {
            HttpResponseMessage response;
            if (id == null){
                throw new NullReferenceException("simulacionId");
            }
                var simulacion = SimulacionService.GetById(Convert.ToInt32(id));
                if (simulacion != null)
                    response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, simulacion);
                else
                    response = ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError,"simulacion no existe");
           return response;        
        }
        [HttpGet]
        public HttpResponseMessage GetAllSimulacion()
        {
            var simulaciones = new List<SimulacionDto>();
            if (User.IsInRole("Jefe"))
                simulaciones = SimulacionService.GetAll().ToList();
            else
                simulaciones = SimulacionService.GetMany(s => s.Empleado.Legajo == User.Identity.Name).ToList();
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, simulaciones);
            return response;
        }        
    }
}
