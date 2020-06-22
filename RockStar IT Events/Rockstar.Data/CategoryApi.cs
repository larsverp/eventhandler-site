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
        private readonly HttpClient client;

        public CategoryApi(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            using (HttpResponseMessage response = await client.GetAsync("categories"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Category>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<List<Category>> GetAllCategoriesFromEvent(string eventId)
        {
            using (HttpResponseMessage response = await client.GetAsync($"categories/event/{eventId}"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Category>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<Category> GetCategory(string categoryId)
        {
            using (HttpResponseMessage response = await client.GetAsync($"users/categories/{categoryId}"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Category>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task CreateCategory(Category category, string bearerToken)
        {
            string json = JsonConvert.SerializeObject(category);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PostAsync("categories", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task UpdateCategory(Category updatedCategory, string bearerToken)
        {
            var json = JsonConvert.SerializeObject(updatedCategory);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await this.client.PutAsync($"categories/{updatedCategory.id}", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task RemoveCategory(string categoryId, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await this.client.DeleteAsync($"categories/{categoryId}"))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }
    }
}