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
    public class SimulacionService : EntityService<Simulacion,SimulacionDto>, ISimulacionService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IValidator<Simulacion> Validator { get; set; }
        
        public SimulacionService(IUnitOfWork unitOfWork,IValidator<Simulacion> validator)
            : base(unitOfWork, validator)
        {
            this.UnitOfWork = unitOfWork;
            this.Validator = validator;
        }


       
    }
}
