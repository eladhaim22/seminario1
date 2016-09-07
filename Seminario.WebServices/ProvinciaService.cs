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
	public class ProvinciaService : EntityService<Provincia, ProvinciaDto>, IProvinciaService
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public ProvinciaService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			this.UnitOfWork = unitOfWork;
		}
	}
}
