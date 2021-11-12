using AutoMapper;
using NLayerBestPractice.API.DTOs;
using NLayerBestPractice.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractice.API.Mapping
{
    public class MapProfile : Profile
    {
        //map icin hangi objeden hang objenin üuretilecegini maplenecegini  buradan veirriz
        public MapProfile()  //bu isleri constuctor icinde yapılacak
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<ProductWithCategoryDto, Product>();
        }
    }
}
