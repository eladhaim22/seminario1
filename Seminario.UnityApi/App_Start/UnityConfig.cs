using Microsoft.Practices.Unity;
using Seminario.NHibernate;
using Seminario.WebServices;
using System.Web.Http;
using Unity.WebApi;

namespace Seminario.UnityApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
           
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
            new InjectionConstructor());
            container.RegisterType<ISimulacionService, SimulacionService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IProvinciaService, ProvinciaService>();
            container.RegisterType<IDatosTTService, DatosTTService>();
            container.RegisterType<IEmpleadoService, EmpleadoService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            
        }
    }
}