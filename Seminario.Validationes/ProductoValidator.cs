using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seminario.Validationes
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {

        }
    }
}
