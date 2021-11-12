using NLayerBestPractice.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);
        //Category özgü methodlarınız varsa burada tanımlayabilirsiniz. helper larda burada yapılablir
    }
}
