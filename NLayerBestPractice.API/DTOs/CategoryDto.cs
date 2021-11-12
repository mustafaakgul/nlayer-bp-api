using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.API.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]   //required clienttan gelecek datada name kesnlkle olsun backend control
        public string Name { get; set; }
    }
}
