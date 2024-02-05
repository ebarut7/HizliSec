using HizliSec.Core.Entities;
using System.Linq.Expressions;

namespace HizliSec.Core.DataAccess
{
    public interface IRepositoryBase<T> where T: class,IEntity,new()
    {
        Task<T> GetAsync(Expression<Func<T,bool>> expression);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
