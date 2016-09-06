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
    public class ProvinciaService : EntityService<Provincia,ProvinciaDto>, IProvinciaService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        
        public IValidator<Provincia> Validator { get; set; }
        
        public ProvinciaService(IUnitOfWork unitOfWork,IValidator<Provincia> validator)
            : base(unitOfWork,validator)
        {
            this.UnitOfWork = unitOfWork;
             this.Validator = validator;
        }


    }
}
