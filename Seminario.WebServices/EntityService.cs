using Seminario.Model;
using Seminario.NHibernate;
using Seminario.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices
{
    public abstract class EntityService<T> : IEntityService<T> where T : Entity
    {
        IUnitOfWork _unitOfWork;

        public EntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _unitOfWork.Repository<T>().Add(entity);
            _unitOfWork.Save();
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.Repository<T>().Update(entity);
            _unitOfWork.Save();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.Repository<T>().Remove(entity);
            _unitOfWork.Save();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _unitOfWork.Repository<T>();
        }

        public virtual T Get(Expression<System.Func<T, bool>> expression)
        {
            return _unitOfWork.Repository<T>().Where(expression).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetMany(Expression<System.Func<T, bool>> expression)
        {
            return _unitOfWork.Repository<T>().Where(expression).ToList();
        }
        
        public virtual T GetById(int id)
        {
            return _unitOfWork.Repository<T>().GetById(id);
        }
    }
} 
