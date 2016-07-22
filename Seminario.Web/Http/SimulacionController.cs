using Seminario.Dto;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.Web.Models;
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
        public IRepository<Simulacion> RepositorySimulacion { get; set; }
        public IRepository<Cheque> RepositoryCheque { get; set; }
        public IRepository<Empleado> RepositoryEmpleado { get; set; }
        public SimulacionController(IUnitOfWork unitOfWork, IRepository<Producto> repositoryProducto,
            IRepository<Provincia> repositoryProvincias, IRepository<Simulacion> repositorySimulacion,
            IRepository<Cheque> repositoryCheque,IRepository<Empleado> repositoryEmpleado)
        {
            this.UnitOfWork = unitOfWork;
            this.RepositorySimulacion = repositorySimulacion;
            this.RepositoryProvincias = repositoryProvincias;
            this.RepositoryProducto = repositoryProducto;
            this.RepositoryCheque = repositoryCheque;
            this.RepositoryEmpleado = repositoryEmpleado;
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostSimulacion(bigData data)
        {
            var simulacion = MapperProject.AutoMapperConfig.mapper.Map<SimulacionDto, Simulacion>(data.Simulacion);
            simulacion.Cheques = new List<Cheque>();
            var listaCheque = data.Cheques.Where(x => x.Documento != null && x.Plazo != 0);
            foreach (var cheque in listaCheque)
            {
                simulacion.Cheques.Add(MapperProject.AutoMapperConfig.mapper.Map<ChequeDto, Cheque>(cheque));
            }
            simulacion.NetoTotal = 0;
            simulacion.SpreadTotal = 5;
            simulacion.Provincia = RepositoryProvincias.GetById(1);
            simulacion.Producto = RepositoryProducto.GetById(1);
            simulacion.Empleado = RepositoryEmpleado.Get(x => x.Nombre == User.Identity.Name);
       
            simulacion.FechaDescuento = DateTime.Now;
            RepositorySimulacion.Add(simulacion);
            UnitOfWork.Commit();
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        
    }
    public class bigData
    {
        public SimulacionDto Simulacion { get; set; }
        public IList<ChequeDto> Cheques { get; set; }
    }

}
