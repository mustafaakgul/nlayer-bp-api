using NLayerBestPractice.Core.Repositories;
using NLayerBestPractice.Core.Services;
using NLayerBestPractice.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Service.Services
{
    //degisikliklerin oldugu yerde commit atılır write da yani
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        //dependency injection objeleri
        public readonly IUnitOfWork _unitOfWork;  //db ye yanstmak icin
        private readonly IRepository<TEntity> _repository;  //db ile islem yapılacagından dolayı baskas yerden ulasılmayacak
        //burada tentity product categry ne gelirse ona gore isler

        public Service(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            //constructor geciyoruz her yerde kullanmak icin
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);   //kaydet snrada db ye yansıtmak icin alttaki kullnılcak

            await _unitOfWork.CommitAsync();  //savechanges gibi kaydetmek icin

            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);

            await _unitOfWork.CommitAsync();

            return entities;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);

            _unitOfWork.Commit();   //db ile ilgi write islemi oldugunda kaydetmek zrndayız
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            _unitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //SingleOrDefaultAsync(x=>x.id==22)
            return await _repository.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            TEntity updateEntity = _repository.Update(entity);

            _unitOfWork.Commit();

            return updateEntity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Where(predicate);
        }
    }
}
