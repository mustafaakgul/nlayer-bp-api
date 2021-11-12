using Microsoft.EntityFrameworkCore;
using NLayerBestPractice.Core.Models;
using NLayerBestPractice.Data.Configurations;
using NLayerBestPractice.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerBestPractice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Tablolara karsı gelen dbsetlerdir
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Person> Persons { get; set; }

        //Db de tablolar olusturulmadan onceki method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //el ile olsuturmak istersek test olarak asagıda 1 satır denem
            //modelBuilder.Entity<Product>().Property(x => x.Id).UseIdentityColumn;
            //2 configurasyon dosyası olusturduk buraya yazmamak cini ayrı yazalım diye 2 satır alttaki kod ile dbocntext temiz tutuldu
            modelBuilder.ApplyConfiguration(new ProductConfiguration());  
            //bu entity ait configurasyon dosyasını farklı bir yer yerine direk buradada yapabliriz ama duzenli olsn diye ayrı yazdık
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2 }));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2 }));

            //farkli olsn diye herseyi brada yapalm person a ozel
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(100);

            modelBuilder.Entity<Person>().Property(x => x.SurName).HasMaxLength(100);
        }
    }


}
