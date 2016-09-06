using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.WebServices.Contracts;

namespace Seminario.WebServices
{
	public class EmpleadoService : EntityService<Empleado, EmpleadoDto>, IEmpleadoService
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public IValidator<Empleado> Validator { get; set; }

		public EmpleadoService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			this.UnitOfWork = unitOfWork;
		}
	}
}
