using Microsoft.EntityFrameworkCore;
using NLayerBestPractice.Core.Models;
using NLayerBestPractice.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Data.Repositories
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _appDbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
