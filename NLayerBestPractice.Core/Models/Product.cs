using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerBestPractice.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }   //defualt olarak false zaten geliyor, ayrıca clientlara data dnerken dto oluascak buradada isdeeleted gozukmesine grek yok
        public string InnerBarcode { get; set; }
        public virtual Category Category { get; set; }  //entity frame bu categry ustunden inherite yapacak deskleri izleyecek tracking ypacak
    }
}
