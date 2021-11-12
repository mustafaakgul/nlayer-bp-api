using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerBestPractice.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerBestPractice.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        //dbcontext icin configure metodu zrunlu ve teme bir metod
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //throw new NotImplementedException();

            builder.HasKey(x => x.Id);  //id alanı primary key olacak
            builder.Property(x => x.Id).UseIdentityColumn();   //1 er otomatk artsin

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();

            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");  //toplam 18 karekter virgul snra 2 karekter 16 virgul once

            builder.Property(x => x.InnerBarcode).HasMaxLength(50);

            builder.ToTable("Products");
        }
    }
}
