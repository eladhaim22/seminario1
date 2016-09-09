using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seminario.Model;

namespace Seminario.Validationes
{
	public class ProvinciaValidator : EntityValidator<Provincia>
	{
		public ProvinciaValidator()
		{
			RuleFor(x => x.Nombre).NotEmpty().NotNull().WithLocalizedMessage(() => "El datoTT debe tener nombre");
			RuleFor(x => x.Sellado).NotEmpty().NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).WithLocalizedMessage(() => "El sellado debe ser entre 0-100");
		}
	}
}
