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
            string t =
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIyIiwianRpIjoiMGQ5OWJlNTUxYjk5MGMyMGVhM2RjYWZkMzUxMWU1ZjYyYTk3MzQ4YzE2ZmQzZDJlYTk4MWEzNzdjYTIwOGYwNWM5ZjUyYWE5YjBmMDBmNzUiLCJpYXQiOjE1OTA0ODU4OTAsIm5iZiI6MTU5MDQ4NTg5MCwiZXhwIjoxNjIyMDIxODkwLCJzdWIiOiIiLCJzY29wZXMiOlsiZ3Vlc3QiXX0.K_PDx95oIRG1P90X__EW65dbXxRbHSz-uuDBeI--rhyJ1UgEiXuVwjvGhdXZ0lo10LTIWgtGkB-62J4S_U2_W4Gm1OjvA2k22QFzCsJ_mS0lxatlJbIWiwyB-LYvQP1sZBdJvNR5YBDchNEsHy160SdMxr1SI0C-f662zBYDO21nY-z1j-zbyTElWlO9TalJ2EAq8RPxTKSQrAFrCQA-crOxHVWCEXM2TMVYLigQX_fTQkqRmpyljYw6NagVrO_acMHFivEGnzjPCkG8BKgbYGLjaXXULttZMNAZ_933L2pxYV_VqXbyIwSVwk0Mg1EIAAhg3uw-Otjw18yBRzITs16mgd6hQPuy2bve7iZtOUwdpoECrlGDrobAFLCOOVwYow1IIIAVQ8S3s3sOadaXFNDkrljiOygIgBAqlgOqJMY-_YwRxHEnWZaddafNo9V84miYra-suunU8r2dpVOmd-3xkkO1VTyZonvbt_8-wZ4A-sQV17NqfZcQWuuJ1wPE6LwTrQl9NwgXmWn-q9O_nLztyxz8HEF3QQwlxtQ4JzDx_q4MLe19nCDJQmJWsOJESi_PCO8x95biqebC7jsQuJcAdSaje6aXQomgAcFE8eb0q30lB0IYUUl3KJUtkBcssLeKeRa3hMyfzVCBJurX3956SYPPHNyRU86wRq7u9rY";

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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", t);

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

        public async Task UpdateUser(User updatedUser, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(updatedUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/users/" + updatedUser.Id;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PutAsync(url, data);
                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
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