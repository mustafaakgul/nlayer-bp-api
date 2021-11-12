using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.Web.DTOs
{
    public class ProductDto
    {
        //productupdatedto kullanırsan buradaki id olmamalı
        public int Id { get; set; }

        //Filters
        //required bir filtredir annotation olarak eklenir bunlar iclerine extra kod calıstırmaya yarar
        //{0} bu gelen alanı ifade eder
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Name { get; set; }

        //min deger 1 max deger int alabilecegidir  farklı olursa hata verecek demek
        [Range(1, int.MaxValue, ErrorMessage = "{0} alanı 1'den büyük bir değer olmalıdır.")]
        public int Stock { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "{0} alanı 1'den büyük bir değer olmalıdır.")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
