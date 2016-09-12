using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.WebServices.Contracts;

namespace Seminario.WebServices
{
	public class SimulacionService : EntityService<Simulacion, SimulacionDto>, ISimulacionService
	{
		private readonly int limiteOperacionesBancoCentral = 300000;

		public IUnitOfWork UnitOfWork { get; set; }

		public SimulacionService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			this.UnitOfWork = unitOfWork;
		}

		public override void Create(SimulacionDto entity)
		{
			entity.FechaCreacion = DateTime.Now;
			entity.FechaUltimaModificacion = DateTime.Now;
			base.Create(entity);
		}

		public override void Update(SimulacionDto entity)
		{
			entity.FechaUltimaModificacion = DateTime.Now;
			base.Update(entity);
		}

		public override void Delete(SimulacionDto entity)
		{
			entity.FechaUltimaModificacion = DateTime.Now;
			base.Delete(entity);
		}
	}
}
