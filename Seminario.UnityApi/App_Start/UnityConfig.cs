using FluentValidation;
using Microsoft.Practices.Unity;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.Validationes;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;
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
           
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<ISimulacionService, SimulacionService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IProvinciaService, ProvinciaService>();
            container.RegisterType<IDatosTTService, DatosTTService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmpleadoService, EmpleadoService>(new ContainerControlledLifetimeManager());
            RegisterValidators(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            
        }
        private static void RegisterValidators(IUnityContainer container)
        {
            container.RegisterType<IValidator<Simulacion>, SimulacionValidator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IValidator<Producto>, ProductoValidator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IValidator<Provincia>, ProvinciaValidator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IValidator<DatosTT>, DatosTTValidator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IValidator<Empleado>, EmpleadoValidator>(new ContainerControlledLifetimeManager());
        }
    }
}