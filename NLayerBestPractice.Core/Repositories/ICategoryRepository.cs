using NLayerBestPractice.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);  
        //buna ek olarak digernde miras alarak onu kullanırız ona ek bununda categry ile sadece ilgil bu metod olacagı icin
    }
}
