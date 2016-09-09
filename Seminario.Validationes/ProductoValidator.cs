using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seminario.Model;

namespace Seminario.Validationes
{
	public class ProductoValidator : EntityValidator<Producto>
	{
		public ProductoValidator()
		{
			RuleFor(x => x.CodigoProducto).NotEmpty().NotNull().WithLocalizedMessage(() => "Debe indicar el codigo de producto");
			RuleFor(x => x.Nombre).NotEmpty().NotNull().WithLocalizedMessage(() => "El producto debe tener nombre");
		}
	}
}
