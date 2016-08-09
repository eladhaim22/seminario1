using Seminario.Model;
using Seminario.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices
{
    public class SimulacionService : EntityService<Simulacion>, ISimulacionService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        
        public SimulacionService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        
    }
}
