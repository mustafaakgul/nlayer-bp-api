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
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _unitOfWork.categories.GetWithProductsByIdAsync(categoryId);
        }
    }
}
