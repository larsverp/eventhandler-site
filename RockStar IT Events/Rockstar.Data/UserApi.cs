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
            var client = new HttpClient();
            
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
        }
    }
}