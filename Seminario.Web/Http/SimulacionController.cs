using Seminario.Dto;
using Seminario.Model;
using Seminario.NHibernate;
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
    public class SimulacionController : ApiController
    {

        public IUnitOfWork UnitOfWork { get; set; }
        public IRepository<Producto> RepositoryProducto { get; set; }
        public IRepository<Provincia> RepositoryProvincias { get; set; }
        
        public HttpResponseMessage PostSimulacion(SimulacionDto data)
        {
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }
    }

}
