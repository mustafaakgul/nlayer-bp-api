using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerBestPractice.API.DTOs;
using NLayerBestPractice.Core.Models;
using NLayerBestPractice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.API.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/[controller]/[action]")]     best pracice acısından uygun degil ama action snuna koyunca url dede getall sonuna yazmak demek
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;   //sadece servis ile haberlesecegiz
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var categories = await _categoryService.GetAllAsync();

            //return Ok(categories);    //noral bu donulur ama bu butun objeyi doner ben butun alanları donmek istemedgmden categry dto yapıp mapleyecegm
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));  //entity dto cift taraflı donusum mekanzması, geriye ctegrydto dncez
            //tek tek eslemek yerine mapper ile objeleri donusturmek yerine maplenir
            //mapper ile ilgili mapster biraz daha lighweight var ve daha iysi automapper
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            //burada o id ait kategry varmıyı burada kontrol etmek yerine filter yaz global butun modellere ait kontrol et
            //her action icinde yazmaktansa
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);

            return Ok(_mapper.Map<CategoryWithProductDto>(category));  //bu geriye productları donemsini istedigimiz icin
            //geriye categrydto da productlar yok o yuzden farklı bir dto nesnesi olusturacagız bu metodda geri donenlere uygun olarak
            //geriye kategorinni productları donecek giris olarak categry verecegiz nesnesini
            //test
        }

        //{"Name" : "silgi"} BODY RAW JSON POST
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));  //buraad dto dan categry dnusturup

            return Created(string.Empty, _mapper.Map<CategoryDto>(newCategory));   //burada enttyden dto dnusturup usera geri dnduk
        }

        //updat ebest practice bisey geri donmez
        //{"Id":3, "Name":"silgiler"} Put  Return type 204 no content
        [HttpPut]
        public IActionResult Update(CategoryDto categoryDto)

        {
            var category = _categoryService.Update(_mapper.Map<Category>(categoryDto));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);

            return NoContent();  //silmedede bisey dnmeye gerek yok 200s don no contnet olanı
        }
    }
}
