using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Seminario.Model;

namespace Seminario.Validationes
{
	public class ServiceLocator
	{
		private static readonly ServiceLocator instance = new ServiceLocator();
		private UnityContainer Ioc = new UnityContainer();

		static ServiceLocator()
		{
		}

		private ServiceLocator()
		{
			Bootstrap();
		}

		public static ServiceLocator Instance
		{
			get
			{
				return instance;
			}
		}

		private void Bootstrap()
		{
			Ioc.RegisterType<IEntityValidator<Simulacion>, SimulacionValidator>(new ContainerControlledLifetimeManager());
			Ioc.RegisterType<IEntityValidator<DatosTT>, DatosTTValidator>(new ContainerControlledLifetimeManager());
			Ioc.RegisterType<IEntityValidator<Producto>, ProductoValidator>(new ContainerControlledLifetimeManager());
			Ioc.RegisterType<IEntityValidator<Provincia>, ProvinciaValidator>(new ContainerControlledLifetimeManager());
			Ioc.RegisterType<IEntityValidator<Empleado>, EmpleadoValidator>(new ContainerControlledLifetimeManager());
		}

		public IEntityValidator Resolve(Type entityType)
		{
			var validatorType = typeof(IEntityValidator<>).MakeGenericType(entityType);
			return Ioc.Resolve(validatorType) as IEntityValidator;
		}
	}
}
