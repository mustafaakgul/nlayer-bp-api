using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NLayerBestPractice.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) //client gecersiz bir durum yazmıssa valisasyon felan yapmamıssa alanı eksk yollamsussa
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 400;   //client hataları 400 ile donecek

                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(v => v.Errors);

                modelErrors.ToList().ForEach(x =>
                {
                    errorDto.Errors.Add(x.ErrorMessage);  //ne kadar hata varsa add ile eklencek
                });

                context.Result = new BadRequestObjectResult(errorDto);
            };
        }
    }
}
