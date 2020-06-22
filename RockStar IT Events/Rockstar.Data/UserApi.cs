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
        private readonly HttpClient client;

        public UserApi(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<string> Login(string username, string password)
        {
            string t = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIyIiwianRpIjoiMzA5NzYwMjQ1NjkxZjZjNTcxOWJiYWYyOTE1ZmFiMDU2NzM3N2U2ZTU3MjBhNjBhYWIwZmIzMjc4Y2Y1YjVkN2U3NGFkNjU0NDIxZTU0NWMiLCJpYXQiOjE1OTA2NTAwMTksIm5iZiI6MTU5MDY1MDAxOSwiZXhwIjoxNjIyMTg2MDE5LCJzdWIiOiIiLCJzY29wZXMiOlsiZ3Vlc3QiXX0.MKWRFv7Y8QbneJab2-3EVSb1edhWNC_u3KBnIZlYCAYlpF3M34Q_TX2Eu5rH-XlOqk1yiucxYo7WKJ6ojgCkPZO_-kkeGFRmc5hgEab1d7EaKABHC7GsrB6LvxPsc6BT3aWJsdSji-RC0WgEVuz7XQYOj-2p9X2uef65ILCcBIzxwG_Az__TyVn8KfqIOoZkQEAB67n94Lm2k5t87Gq4I02zwL4baFXYDrl6HAVaNmedY4OhnpsAIyCaBKJSU3-ql2T7P4i86GAEU7K45qr0xhhvZ7ktUsC60NQQAjAiSwKxwHFResgWcRhIZ42QqXIcPCsFDFtV6WsyPHIxlcUJv8XCWOwIrsDvrXAijQ0uIP9WSUecH3Fmb_jmmSWshjEFziRUeWXlUgAk4x6h_-H54YFT57Do1UTFt87aSYcs9UuwlcnMs--xiwZA34YgijFYCwLVKTFcCZa4H2CyfnPUUv7n8gKGhiQDvA6iNrmZXi0mj-DMjTNT4WzMtMuz7vNZo6JxXRkEJBwg9srvjUFDqWewFvEeM6rjv5CMBz7bhOy3Ok4kkE1NkY-Z6xNFiW1YMxV35XPIuG58AOG-hoG3MbMyofxQA_YASgyj_a2q-w8hk74Z4XbdOIFTLRPS6EixOkYzSktIAvpZQ5AjWfN0iRgJami39i1gRjBqS_FmChI";

            var userDetails = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", t);

            string json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync("users/login", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsAsync<Token>();
                    return token.access_token;
                }

                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task Signup(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync("users/register", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<string> GetRole(string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.GetAsync("users/role"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var role = await response.Content.ReadAsAsync<Role>();
                    return role.role;
                }

                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task RemoveUser(string userId, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.DeleteAsync($"users/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<List<User>> ReturnAllUsers(string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.GetAsync("users"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<User>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task UpdateUser(UserWithoutPassword updatedUser, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var json = JsonConvert.SerializeObject(updatedUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PutAsync($"users/{updatedUser.Id}", data))
            {
                if(response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task ValidateUser(string email, string token)
        {
            var userDetails = new Dictionary<string, string>
            {
                { "email", email },
                { "token", token}
            };

            string json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync("users/validation", data))
            {
                if (response.IsSuccessStatusCode) 
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }
    }

    class Role
    {
        public string role { get; set; }
    }
}