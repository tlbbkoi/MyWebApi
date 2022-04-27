using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyWebApi.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T,bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> oderBy = null,
            List<string> includes = null
            );

        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task Insert(T entity);

        Task CreateRange(IEnumerable<T> entities);

        Task Delete(int id);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}
