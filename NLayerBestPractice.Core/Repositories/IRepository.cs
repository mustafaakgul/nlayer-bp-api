using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace NLayerBestPractice.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity:class  //alınana generic class olsun
    {
        Task<TEntity> GetByIdAsync(int Id);  //asenkron olacak
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);  //tentity alan geriye bool donen
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);  //x=>x.name="kalem"
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);  //cogul olablr
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);  //brden fazla slinme icin
        TEntity Update(TEntity entity);
    }
}
