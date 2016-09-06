using FluentValidation;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.WebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices
{
    public class EmpleadoService : EntityService<Empleado,EmpleadoDto>, IEmpleadoService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IValidator<Empleado> Validator { get; set; }

        public EmpleadoService(IUnitOfWork unitOfWork, IValidator<Empleado> validator)
            : base(unitOfWork,validator)
        {
            this.UnitOfWork = unitOfWork;
            this.Validator = validator;
        }
        
    }
}
