using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NLayerBestPractice.Web.DTOs;

namespace NLayerBestPractice.Web.ApiService
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient. bunun ile hangi methodları gerceklestirebilecegimizi bulabiriz
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()  //kendi yazacagımız asnekron maetodlara mutlaka async eklemek bestpractice
        {
            IEnumerable<CategoryDto> categoryDtos; //snucu karsılamak icin donecek veri formatı esitlemek icin

            var response = await _httpClient.GetAsync("categories");   //http kendi matodlarını kullancaz bu base url sonuna gelcek string istek url i bu

            if (response.IsSuccessStatusCode) //istek basarılı ise
            {
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
                //yukarda glen json objesi ilk ıenumarable snra categorydto dnusturulcek gibi
            }
            else
            {
                categoryDtos = null;
            }

            return categoryDtos;
        }

        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("categories", stringContent);

            if (response.IsSuccessStatusCode)  //200 baslayanda buraya girer
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());

                return categoryDto;
            }
            else
            {
                //loglama yap
                return null;
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        //update ten geriye bisey dnmemek lazım bunn gibi
        public async Task<bool> Update(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("categories", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            {
                return false;
            }
        } 
    }
}
