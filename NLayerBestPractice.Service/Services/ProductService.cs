using NLayerBestPractice.Core.Models;
using NLayerBestPractice.Core.Repositories;
using NLayerBestPractice.Core.Services;
using NLayerBestPractice.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        //service<product> dan dolayı onun icinekilerin hepsi geldi
        //IUnitofwork dependecy injec direk bir nesne olusuturp bize verecek
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await _unitOfWork.Products.GetWithCategoryByIdAsync(productId);
            //service.cs unitofwork public yapılmıstı burada kullanılmak istendigi icin
            //_unitOfWork.Products direk product donuyor icinde ?? yoksa bile product dondur dedigi icin
        }
    }
}
