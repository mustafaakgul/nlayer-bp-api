using NLayerBestPractice.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//buna neden ihtyiac var sadece producta ozgu bir sey icin ihtiyac oluyor
namespace NLayerBestPractice.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);

        // bool ControlInnerBarcode(Product product);  //inner barcod msela control etmemz gerekli ise bunun db ile ilgisi yok
        // bu yuzden service leri olsuturuyoruz db dısında bir işlem yapmak gerekli ise
        //repo daki fonk copy paste yapabliriz
    }
}
