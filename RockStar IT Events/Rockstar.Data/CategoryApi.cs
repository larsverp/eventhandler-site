using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rockstar.Models;

namespace Rockstar.Data
{
    public class CategoryApi
    {
        public async Task<List<Category>> GetAllCategories()
        {
            var url = "https://eventhandler-api.herokuapp.com/api/categories";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);
                string result = response.Content.ReadAsStringAsync().Result;
                var categories = JsonConvert.DeserializeObject<List<Category>>(result);
                return categories;
            }
        }

        public async Task<List<Category>> GetAllCategoriesFromEvent(string eventId)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/categories/event/" + eventId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);
                string result = response.Content.ReadAsStringAsync().Result;
                var categories = JsonConvert.DeserializeObject<List<Category>>(result);
                return categories;
            }
        }

        public async Task<Category> GetCategory(string categoryId)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users/categories/" + categoryId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);
                string result = response.Content.ReadAsStringAsync().Result;
                var category = JsonConvert.DeserializeObject<Category>(result);
                return category;
            }
        }

        public async Task CreateCategory(Category category, string cookieValue)
        {
            string json = JsonConvert.SerializeObject(category);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://eventhandler-api.herokuapp.com/api/categories";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task UpdateCategory(Category updatedCategory, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(updatedCategory);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/categories/" + updatedCategory.id;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            var response = await client.PutAsync(url, data);

            if(response.IsSuccessStatusCode == false)
                throw new ArgumentException("Something went wrong");
        }

        public async Task RemoveCategory(string categoryId, string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/categories/" + categoryId;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            var response = await client.DeleteAsync(url);
        }
    }
}