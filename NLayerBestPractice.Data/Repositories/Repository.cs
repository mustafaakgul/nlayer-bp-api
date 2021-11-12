using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NLayerBestPractice.Core.Repositories;

namespace NLayerBestPractice.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        //(AppDbContext context)
        public Repository(AppDbContext context)
        {
            _context = context;   //bu obje ile db ye erisim gerceklestirriz
            _dbSet = context.Set<TEntity>();    //bu obje ile tablolora erisiriz
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);    //await bundan sonra yazacagm method bitene kdar bu satırda bekle
            //saveChanges();    burada save changes yazılmaz cnku unit of work pattern kullanıroyurz tek tek hepsine save changes yerine
            //transaction yapısını kullanabilecegimiz bir mantık ile gidilecek
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        //product.where(x=>x.name="kalem"
        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()    //Task senkron programlamadaki void
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);  //ilk gelen kaydı getir yoksa defaultunu getr
        }

        //bu butun alanların satırlarını tek tek yazmadan kendisi yazmasını saglar generic
        //entity.name=product.name
        //entity.price=product.price gibi bir suru alanı kendi yapar
        //dezavantajı ise tek bir alanı degisitrsen bile hepsini aynı olsa bile tek tek yazar degistirir
        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }      
    }
}
