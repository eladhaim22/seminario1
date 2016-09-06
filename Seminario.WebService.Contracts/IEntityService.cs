using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices.Contracts
{
    public interface IEntityService<T,TDto>
        where T : Entity
        where TDto : EntityDto
    {
        void Create(TDto entity);
        void Delete(TDto entity);
        IEnumerable<TDto> GetAll();
        void Update(TDto entity);
        TDto Get(Expression<System.Func<T, bool>> expression);
        IEnumerable<TDto> GetMany(Expression<System.Func<T, bool>> expression);
        TDto GetById(int id);
    }
}