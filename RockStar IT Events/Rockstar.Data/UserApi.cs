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
    public class UserApi
    {
        public async Task<string> Login(string username, string password)
        {
            var userDetails = new Dictionary<string, string>()
            {
                { "username", username },
                { "password", password }
            };

            string json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://eventhandler-api.herokuapp.com/api/users/login";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            string token = JsonConvert.DeserializeObject<Token>(result).access_token;
            
            return token;
        }

        public async Task Signup(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://eventhandler-api.herokuapp.com/api/users/register";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<string> GetRole(string token)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users/role";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);
            string result = response.Content.ReadAsStringAsync().Result;
            string role = JsonConvert.DeserializeObject<Role>(result).role;
            return role;
        }

        public async Task RemoveUser(string userId, string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users/" + userId;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            var response = await client.DeleteAsync(url);
        }

        public async Task<List<User>> ReturnAllUsers(string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/users";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            try
            {
                var response = await client.GetStringAsync(url);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(response);

                return users;
            }
            catch
            {
                return new List<User>();
            }
        }

        public async Task ValidateUser(string email, string token)
        {
            var userDetails = new Dictionary<string, string>()
            {
                { "email", email },
                { "token", token}
            };

            string json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/users/validation";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, data);
                if(response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }
    }

    class Role
    {
        public string role { get; set; }
    }
}