using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerBestPractice.API.DTOs;
using NLayerBestPractice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.API.Filters
{
    //bunun contructrda bir nesne adıgından startup tanımlanmalı almasaydı diger filtredeki gibi tanımlamayacaktık
    //updat edelete ilgili id varmı gibi seyleri kontrol eden mekanizma
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly IProductService _productService;
        //yukardakinin yerine sylede kullanılablir private readonly IService<Product>

        public NotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();  //id yi yakalamak icin

            var product = await _productService.GetByIdAsync(id);

            if (product != null)
            {
                await next();  //sorun yoksa kodu devam ettir bi problem yok
            }
            else
            {  //id ye sahip urun yoksa
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 404;   //client hatası 400s, 404 buna en uygun

                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
