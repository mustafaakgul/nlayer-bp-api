using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerBestPractice.Core.Models;

namespace NLayerBestPractice.Data.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        private readonly int[] _ids;

        public ProductSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //default olarak data basıyorsan herseyi vereceksin id yi felanda veriyorsun bu tarafta otomatk artma olayı felan yemez
            //direk db ye gidicek cnku
            //CategoryId = _ids[0] bu ifade burya yukardan gelen id lerden 0 incisini direk bassın
            builder.HasData(
                new Product { Id = 1, Name = "Pilot Kalem", Price = 12.50m, Stock = 100, CategoryId = _ids[0] },
                    new Product { Id = 2, Name = "Kurşun Kalem", Price = 40.50m, Stock = 200, CategoryId = _ids[0] },
                        new Product { Id = 3, Name = "Tükenmez Kalem", Price = 500m, Stock = 300, CategoryId = _ids[0] },
                            new Product { Id = 4, Name = "Küçük Boy Defter", Price = 12.50m, Stock = 100, CategoryId = _ids[1] },
                                  new Product { Id = 5, Name = "Orta Boy Defter", Price = 12.50m, Stock = 100, CategoryId = _ids[1] },
                                        new Product { Id = 6, Name = "Büyük Boy Defter", Price = 12.50m, Stock = 100, CategoryId = _ids[1] }
                );
        }
    }
}
