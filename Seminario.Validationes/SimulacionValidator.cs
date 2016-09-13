using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seminario.Model;

namespace Seminario.Validationes
{
	public class SimulacionValidator : EntityValidator<Simulacion>
	{
		public SimulacionValidator()
		{
			this.FluentValidator.RuleForEach(simulacion => simulacion.Cheques).Must((simulacion, cheque) =>
			{
				if ((cheque.FechaAcreditacion - simulacion.FechaDescuento).TotalMilliseconds > 0)
					return true;
				else
					return false;
			}).WithLocalizedMessage(() => "La fecha de acreditacion de todos los cheques debes ser posterior al feche de descuento");
			RuleFor(x => x.FechaDescuento).NotEmpty().NotNull().WithLocalizedMessage(() => "La fecha de acreditacion no puede quedar vacia");
			RuleFor(x => x.FechaDescuento.Date).GreaterThanOrEqualTo(DateTime.Now.Date).WithLocalizedMessage(() => "La fecha de descuento debe postrior al dia de hoy");
			RuleFor(x => x.TasaIIBB).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).WithLocalizedMessage(() => "La Tasa IIBB debe ser entre 0 y 100");
			RuleFor(x => x.ComisionTotal).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).WithLocalizedMessage(() => "La Comision Administrativa debe ser entre 0 y 100");
			RuleFor(x => x.TNAV).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).WithLocalizedMessage(() => "La TNAv debe ser entre 0 y 100");
			RuleFor(x => x.Provincia).NotNull().NotEmpty().WithLocalizedMessage(() => "Debe elejir sellado de provincia de la lista");
			RuleFor(x => x.Producto).NotNull().NotEmpty().WithLocalizedMessage(() => "Debe elejir producto de la lista");
			RuleFor(x => x.TipoCategoria).NotNull().NotEmpty().WithLocalizedMessage(() => "Debe elejir opcion de iva de la lista");
			RuleFor(x => x.Cheques.Count).GreaterThan(0).WithLocalizedMessage(() => "Debe haber al menos un cheque");
			RuleFor(x => x.Provincia.Id).NotEqual(0).WithLocalizedMessage(() => "Debe elejir una provincia de la lista");
			RuleFor(x => x.Producto.Id).NotEqual(0).WithLocalizedMessage(() => "Debe elejir un producto de la lista");
		}
	}
}
