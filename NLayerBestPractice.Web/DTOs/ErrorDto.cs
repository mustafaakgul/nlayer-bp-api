using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.Web.DTOs
{
    public class ErrorDto
    {
        //Custom error icin clientlara donen yapı
        //bu sadece validasyonlar degil exception felan hepsi bunun json donusuturlmus hali donecek jsonda bir standart oalcak
        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public List<String> Errors { get; set; }
        public int Status { get; set; }
    }
}
