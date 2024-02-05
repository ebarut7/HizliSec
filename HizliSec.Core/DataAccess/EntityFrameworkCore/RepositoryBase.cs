using HizliSec.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace HizliSec.Core.DataAccess.EntityFrameworkCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity, new()
    {
        DbContext _db;
        DbSet<T> _set;
        public RepositoryBase(DbContext db)
        {
            _db = db;
            _set = _db.Set<T>();
        }
        public async Task AddAsync(T entity) => _set.Add(entity);
        public async Task DeleteAsync(T entity) => _set.Remove(entity);
        public async Task UpdateAsync(T entity) => _set.Update(entity);
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null) => expression is not null ? _set.Where(expression).ToList() : _set.ToList();
        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        => await _set.FirstOrDefaultAsync(expression);
        
    }
}
