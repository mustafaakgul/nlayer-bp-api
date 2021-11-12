using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.Web.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} alanı boş olamaz")]   //required clienttan gelecek datada name kesnlkle olsun backend control
        public string Name { get; set; }
    }
}
