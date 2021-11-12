using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NLayerBestPractice.Core.Models
{
    public class Category
    {
        public Category()
        {
            Products = new Collection<Product>();  //bos collection nesnesi olstursun
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; } //her kategory birden fazla product
    }
}
