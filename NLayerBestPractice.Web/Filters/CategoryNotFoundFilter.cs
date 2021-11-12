using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerBestPractice.Web.ApiService;
//using NLayerBestPractice.Core.Services;
using NLayerBestPractice.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.Web.Filters
{
    //bunun contructrda bir nesne adıgından startup tanımlanmalı almasaydı diger filtredeki gibi tanımlamayacaktık
    //updat edelete ilgili id varmı gibi seyleri kontrol eden mekanizma
    public class CategoryNotFoundFilter : ActionFilterAttribute
    {
        //private readonly ICategoryService _categoryService;  //servisten degil api den haberlestigi icin artık bunu kaldırıp edit ypcaz alt satırdan deva metcek
        private CategoryApiService _categoryApiService;
        //yukardakinin yerine sylede kullanılablir private readonly IService<Product>

        /*public CategoryNotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }*/
        public CategoryNotFoundFilter(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();  //id yi yakalamak icin

            //var category = await _categoryService.GetByIdAsync(id);
            var category = await _categoryApiService.GetByIdAsync(id);

            if (category != null)
            {
                await next();  //sorun yoksa kodu devam ettir bi problem yok
            }
            else
            {  //id ye sahip urun yoksa
                ErrorDto errorDto = new ErrorDto();

                errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı");

                context.Result = new RedirectToActionResult("Error", "Home", errorDto);  //home icindeki error fonksiyonu
            }
        }
    }
}
