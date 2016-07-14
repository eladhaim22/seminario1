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
    public class SettingController : ApiController
    {

        public IUnitOfWork UnitOfWork { get; set; }
        public IRepository<Producto> RepositoryProducto { get; set; }

        public SettingController(IUnitOfWork unitOfWork, IRepository<Producto> repositoryProducto, IRepository<Provincia> repositoryProvincias)
        {
            this.RepositoryProvincias = repositoryProvincias;
            this.UnitOfWork = unitOfWork;
            this.RepositoryProducto = repositoryProducto;
        }

        public HttpResponseMessage GetAllProductos()
        {
            var productos = RepositoryProducto.GetAll();
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, productos);
            return response;
        }

        public HttpResponseMessage GetAllProvincias()
        {
            var provincias = RepositoryProvincias.GetAll();
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, provincias);
            return response;
        }

        public IRepository<Provincia> RepositoryProvincias { get; set; }
    }

}
