using Seminario.Model;
using Seminario.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices
{
    public class EmpleadoService : EntityService<Empleado>, IEmpleadoService
    {
        public IUnitOfWork UnitOfWork { get; set; }


        public EmpleadoService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        
    }
}
