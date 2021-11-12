using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.Web.DTOs
{
    //categorydto dan miras aldık onların metodlarınıda kullanalım diye heriye productdto donelim ihtiyac urunler cnku
    public class CategoryWithProductDto : CategoryDto
    {
        public ICollection<ProductDto> Products { get; set; }
    }
}
