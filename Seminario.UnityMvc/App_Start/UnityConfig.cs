using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Seminario.NHibernate;

using Seminario.WebServices;
using Seminario.WebServices.Contracts;
using Seminario.Model;
using Seminario.Validationes;
using FluentValidation;

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
            container.RegisterType<IEmpleadoService, EmpleadoService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDatosTTService, DatosTTService>(new ContainerControlledLifetimeManager());
            RegisterValidators(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            
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