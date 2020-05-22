using System;
using Newtonsoft.Json;
using Rockstar.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Data
{
    public class ReviewApi
    {
        public async Task<List<Review>> GetAllReviews(string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users/reviews";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.GetAsync(url);
                string result = response.Content.ReadAsStringAsync().Result;
                var reviews = JsonConvert.DeserializeObject<List<Review>>(result);
                return reviews;
            }
        }

        public async Task<List<Review>> GetAllReviewsFromEvent(string eventId, string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users/reviews/" + eventId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.GetAsync(url);
                string result = response.Content.ReadAsStringAsync().Result;
                var reviews = JsonConvert.DeserializeObject<List<Review>>(result);
                return reviews;
            }
        }

        public async Task<List<Review>> GetUncheckedReviews(string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users/reviews/check";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.GetAsync(url);
                string result = response.Content.ReadAsStringAsync().Result;
                var reviews = JsonConvert.DeserializeObject<List<Review>>(result);
                return reviews;
            }
        }

        public async Task CreateReview(Review review, string cookieValue)
        {
            string json = JsonConvert.SerializeObject(review);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://eventhandler-api.herokuapp.com/api/reviews";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, data);
                if(response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task ApproveReview(string reviewId, string cookieValue)
        {
            string url = "https://eventhandler-api.herokuapp.com/api/reviews/approve/" + reviewId;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task RemoveReview(string reviewId, string cookieValue)
        {
            string url = "https://eventhandler-api.herokuapp.com/api/reviews/" + reviewId;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }
    }
}