using Seminario.Dto;
using Seminario.Model;
using Seminario.WebServices;
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
        public IProvinciaService ProvinciaService;
        public IProductoService ProductoService;
        public IEmpleadoService EmpleadoService;

        public SimulacionController(ISimulacionService simulacionService, IProvinciaService provinciaService,
            IProductoService productoService, IEmpleadoService empleadoService)
        {
            this.SimulacionService = simulacionService;
            this.ProductoService = productoService;
            this.ProvinciaService = provinciaService;
            this.EmpleadoService = empleadoService;
        }


        [HttpPost]
        public HttpResponseMessage PostSimulacion(SimulacionDto data)
        {
            var simulacion = MapperProject.AutoMapperConfig.mapper.Map<SimulacionDto, Simulacion>(data);
            simulacion.Provincia = ProvinciaService.GetById(data.IdProvincia);
            simulacion.Producto = ProductoService.GetById(data.CodProd);
            simulacion.Empleado = EmpleadoService.Get(empleado => empleado.Legajo == User.Identity.Name);
            ProductoService.Create(simulacion.Producto);
            SimulacionService.Create(simulacion);
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        
    }
}
