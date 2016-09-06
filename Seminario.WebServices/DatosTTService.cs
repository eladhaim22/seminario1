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
    public class DatosTTService : EntityService<DatosTT,DatosTTDto>, IDatosTTService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IValidator<DatosTT> Validator { get; set; }

        public DatosTTService(IUnitOfWork unitOfWork, IValidator<DatosTT> validator)
            : base(unitOfWork,validator)
        {
            this.UnitOfWork = unitOfWork;
            this.Validator = validator;
        }
    }
}
