using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Seminario.NHibernate;
using Seminario.WebServices;

namespace Seminario.UnityMvc
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
            container.RegisterType<IEmpleadoService, EmpleadoService>();
            container.RegisterType<IDatosTTService, DatosTTService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            
        }
    }
}