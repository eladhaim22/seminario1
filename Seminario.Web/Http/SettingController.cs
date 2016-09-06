using Seminario.Model;
using Seminario.NHibernate;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Seminario.Web.Http
{
    public class SettingController : ApiController
    {
        public IProductoService ProductoService;
        public IProvinciaService ProvinciaService;

        public SettingController(IProductoService productoService, IProvinciaService provinciaService)
        {
            this.ProductoService = productoService;
            this.ProvinciaService = provinciaService;
        }

        public HttpResponseMessage GetAllProductos()
        {
            var productos = ProductoService.GetAll().ToList();
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, productos);
            return response;
        }

        public HttpResponseMessage GetAllProvincias()
        {
            var provincias = ProvinciaService.GetAll().ToList();
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, provincias);
            return response;
        }
    
     }

}
