using NLayerBestPractice.Core.Repositories;
using NLayerBestPractice.Core.UnitOfWorks;
using NLayerBestPractice.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ProductRepository _productRepository;
        private CategoryRepository _CategoryRepository;

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);   
        //productrepo objesi varsa al yoksa yani ?? yeni bir productrepo objesi olustur

        public ICategoryRepository categories => _CategoryRepository = _CategoryRepository ?? new CategoryRepository(_context);

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        //artık nerede commit caıgırlırsa db ye yansıması gerceklesecek bunu ayrı yazdık sadece db ye nerde cagırılmasını isiyorsak
        public void Commit()    //bu normaldeki savechanges oluyor bu method senkron asgıdaki asenkron
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()  //yukardaki commitin asenkron olanı
        {
            await _context.SaveChangesAsync();
        }
    }
}
