using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seminario.Model;

namespace Seminario.Validationes
{
	public class ChequeValidator : EntityValidator<Cheque>
	{
		public ChequeValidator()
		{
			RuleFor(x => x.Banco).NotNull().NotEmpty().WithLocalizedMessage(() => "Debe elejir Banco para cada cheque");
			RuleFor(x => x.FechaAcreditacion).NotNull().NotEmpty().WithLocalizedMessage(() => "La fecha de acreditacion debe ser valido");
			RuleFor(x => x.FechaAcreditacion).LessThanOrEqualTo(DateTime.Now).WithLocalizedMessage(() => "La fecha de acreditacion debe ser posterior a la fecha de hoy");
			RuleFor(x => x.NombreEmisor).NotNull().NotEmpty().WithLocalizedMessage(() => "el nombre del emisor debe ser valido");
			RuleFor(x => x.Importe).NotNull().NotEmpty().LessThanOrEqualTo(0).WithLocalizedMessage(() => "el monto del cheque debe ser mayor que 0");
		}
	}
}
