using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seminario.Validationes
{
    public class SimulacionValidator : AbstractValidator<Simulacion>
    {
        public SimulacionValidator()
        {
            RuleFor(x => x.SpreadTotal).GreaterThan(3).WithLocalizedMessage(() => "Exito");
        }
    }
}
