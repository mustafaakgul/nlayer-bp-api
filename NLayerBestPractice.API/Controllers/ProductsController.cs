using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerBestPractice.API.DTOs;
using NLayerBestPractice.API.Filters;
using NLayerBestPractice.Core.Models;
using NLayerBestPractice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //custom exception ile hata fırlatalim global hataya dusecekmi diye global hata diye yazdıgımı sey exception icin calısıyor cnku
            //startapp yazdıgımız cagırıdıgmız
            throw new Exception("error occured while getting all data"); //bu nomalde yoruma al global exception geri donus nesnesini gstermek icin

            var products = await _productService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));  
            //tek tek eslemek yerine mapper ile objeleri donusturmek yerine maplenir
            //mapper ile ilgili mapster biraz daha lighweight var ve daha iysi automapper
        }

        //notfound filtersini calstrmak icin products/55 istek at olmayana gor
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }

        /*
         * name stock price categryid ile post et
         */
        //[ValidationFilter] bunu koydugunda calısması icin save gonder am soyle {"CategoryId":1}
        //donus boyle olur
        /*
         * {
                "errors": [
                    "Name alanı gereklidir",
                    "Price alanı 1'den büyük bir değer olmalıdır.",
                    "Stock alanı 1'den büyük bir değer olmalıdır."
                ],
                "status": 400
            }
         */
        //[ValidationFilter]   //bunu method bagımlı yada controller class bgmlı yapıp sadece bu classda etki etsin
        //yada startupda anımlayıp butun contorllerara etki etsin diyebliriz
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newproduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty, _mapper.Map<ProductDto>(newproduct));  //mapper ile cevirme islemi
        }

        //id name stock price categryid alanları ile put
        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            //Bu cozumde id olmadan gonderilen update icin kullaılablir burdaki exception startup daki exception yakalamacı yakalayıp ona gore
            //standart formatta donus ozelligini acacak
            //best practice acısından attaki if kontrolu olmamalı sadece gstermek icin baktık
            /*if (string.IsNullOrEmpty(productDto.Id.ToString()) || productDto.Id <= 0)//id nin default degeri var cnku 0 calıstırınca ustten debugda bak
            {
                throw new Exception("Id field is necessary");
            }*/

            var product = _productService.Update(_mapper.Map<Product>(productDto));

            return NoContent();
        }

        //burası icin 3 yerde kullandıgımız yer tek yerde merkezilesti a3 kod tekrarından kacındık
        [ServiceFilter(typeof(NotFoundFilter))]   //aslında bu dto bagımlı oldugunda productnotfoundfilter olarak ismini degismesi lazım
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;  //asenkron degilse direk sonucu Result property ile abanırız

            _productService.Remove(product);
            return NoContent();
        }

        //Removerange ve addrange yazılmadı ama arkası hazır controller direk yazılıp apide kullanabliriz
    }
}
