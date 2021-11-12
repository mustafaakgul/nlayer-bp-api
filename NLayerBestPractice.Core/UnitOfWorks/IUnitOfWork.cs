using NLayerBestPractice.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractice.Core.UnitOfWorks
{
    //unit of pattern uygulaması crud yapılarının db ye yansımasını bize bırakması
    //bu pattern yoksa  sırayla farklı tablolara insert yapacaz bi kac tane var ortasnda hata oldu transaction da rollback 
    //yapılması gerkeiyor unit of pattern ise bunları koda yansıt sırayla yapma butun islmler bitsn bir anda yapalım hatayı
    //minimize edelim yani memoryde yapılan tum deigiklikleri istedigimiz yerde bir anda hepsini yansıtalım diyor
    //commit cagırılmadıgı surece bunlar db ye yansımayaaktır unit of pattern bir depencdency inejct olarak imlemente ediyorz
    public interface IUnitOfWork  //burası internal olursa baska class librar den implemente edemeyiz
    {
        IProductRepository Products { get; }     //sadece get olacak product ve categry bunn yerine baska yerdede olablr ama burdada olblr
        ICategoryRepository categories { get; }
        Task CommitAsync(); //async olacak ismede yanssın baskası okudugunda bunun asenkron oldugunu bilsin
        void Commit();
    }
}
