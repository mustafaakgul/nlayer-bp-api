using AutoMapper;
using Microsoft.AspNetCore.Mvc;
//using NLayerBestPractice.Core.Models;
//using NLayerBestPractice.Core.Services;
using NLayerBestPractice.Web.ApiService;
using NLayerBestPractice.Web.DTOs;
using NLayerBestPractice.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.Web.Controllers
{
    //artık burası servise baglanmak yerine direk api projesine baglanıp istek atıp oradan devam edecektr
    public class CategoriesController : Controller
    {
        //private readonly ICategoryService _categoryService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;

        //categry controller olustugu zaman contructredaki 2 nesneyi olsuturcak butun hepsini snrada bizim readonly ile actıklarımıza atayacak
        public CategoriesController(IMapper mapper, CategoryApiService categoryApiService) //ICategoryService categoryService, 
        {
            _mapper = mapper;
            //_categoryService = categoryService;   //uygulama ayaga kalkınca categry service ve crud metodlarımız artık olacak
            _categoryApiService = categoryApiService;
        }

        //https://localhost:44303/categories
        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.GetAllAsync();      //katmanla haberlesiyor eski yontem
            var categories = await _categoryApiService.GetAllAsync();     //api ile haberlesiyor

            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        //eklenin get i ekleye grnce calısacak olan gruntu olan metod sadec epost olan dgl
        public IActionResult Create()
        {
            return View();
        }

        //https://localhost:44303/categories/create
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)  //zna sayfadan kategrydto gelcek
        {
            //await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));  //categrydto yu categry ye dnustur,eski yntem
            await _categoryApiService.AddAsync(categoryDto);  //dto yu drek vrebliriz

            return RedirectToAction("Index");
        }

        //update/5
        public async Task<IActionResult> Update(int id)
        {
            //var category = await _categoryService.GetByIdAsync(id);
            var category = await _categoryApiService.GetByIdAsync(id);

            return View(_mapper.Map<CategoryDto>(category));
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            //_categoryService.Update(_mapper.Map<Category>(categoryDto));
            await _categoryApiService.Update(categoryDto);

            return RedirectToAction("Index");
        }

        //error calismasi icin https://localhost:44303/Categories/Delete/4565
        //ya asenskron await ikilisini kullancaz yada senkron yapıp result diyecegiz
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            //var category = await _categoryApiService.GetByIdAsync(id);

            await _categoryApiService.Remove(id);
            return RedirectToAction("Index");  //geriye saece sayfaya yonlendiricegiz
        }

        /*
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.Remove(id);

            return RedirectToAction("Index");
        }*/
    }
}
