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
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIyIiwianRpIjoiMzA5NzYwMjQ1NjkxZjZjNTcxOWJiYWYyOTE1ZmFiMDU2NzM3N2U2ZTU3MjBhNjBhYWIwZmIzMjc4Y2Y1YjVkN2U3NGFkNjU0NDIxZTU0NWMiLCJpYXQiOjE1OTA2NTAwMTksIm5iZiI6MTU5MDY1MDAxOSwiZXhwIjoxNjIyMTg2MDE5LCJzdWIiOiIiLCJzY29wZXMiOlsiZ3Vlc3QiXX0.MKWRFv7Y8QbneJab2-3EVSb1edhWNC_u3KBnIZlYCAYlpF3M34Q_TX2Eu5rH-XlOqk1yiucxYo7WKJ6ojgCkPZO_-kkeGFRmc5hgEab1d7EaKABHC7GsrB6LvxPsc6BT3aWJsdSji-RC0WgEVuz7XQYOj-2p9X2uef65ILCcBIzxwG_Az__TyVn8KfqIOoZkQEAB67n94Lm2k5t87Gq4I02zwL4baFXYDrl6HAVaNmedY4OhnpsAIyCaBKJSU3-ql2T7P4i86GAEU7K45qr0xhhvZ7ktUsC60NQQAjAiSwKxwHFResgWcRhIZ42QqXIcPCsFDFtV6WsyPHIxlcUJv8XCWOwIrsDvrXAijQ0uIP9WSUecH3Fmb_jmmSWshjEFziRUeWXlUgAk4x6h_-H54YFT57Do1UTFt87aSYcs9UuwlcnMs--xiwZA34YgijFYCwLVKTFcCZa4H2CyfnPUUv7n8gKGhiQDvA6iNrmZXi0mj-DMjTNT4WzMtMuz7vNZo6JxXRkEJBwg9srvjUFDqWewFvEeM6rjv5CMBz7bhOy3Ok4kkE1NkY-Z6xNFiW1YMxV35XPIuG58AOG-hoG3MbMyofxQA_YASgyj_a2q-w8hk74Z4XbdOIFTLRPS6EixOkYzSktIAvpZQ5AjWfN0iRgJami39i1gRjBqS_FmChI";

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

        public async Task UpdateUser(UserWithoutPassword updatedUser, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(updatedUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/users/" + updatedUser.Id;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PutAsync(url, data);
                string x = response.Content.ReadAsStringAsync().Result;
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