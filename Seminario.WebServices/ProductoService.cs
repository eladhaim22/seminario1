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
    public class ProductoService : EntityService<Producto,ProductoDto>, IProductoService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IValidator<Producto> Validator { get; set; }

        public ProductoService(IUnitOfWork unitOfWork, IValidator<Producto> validator)
            : base(unitOfWork,validator)
        {
            this.UnitOfWork = unitOfWork;
            this.Validator = validator;
        }
        
    }
}
