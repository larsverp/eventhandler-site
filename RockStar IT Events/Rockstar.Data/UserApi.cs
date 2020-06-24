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
            string t = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIyIiwianRpIjoiMjI1ZDQ1YWRjMzIyNWU5ODIwYzBiZDJmZTNmMDg3YmE3MmVmYWE1YTg4YTEzNTAzM2RiMzllYzZkNzBhMGRjODE4M2M1YzU5Nzc1ZDJlYmIiLCJpYXQiOjE1OTI5OTgyNTgsIm5iZiI6MTU5Mjk5ODI1OCwiZXhwIjoxNjI0NTM0MjU4LCJzdWIiOiIiLCJzY29wZXMiOlsiZ3Vlc3QiXX0.gexHJdt6UIY7l8Y9E7sT3Eq-X4VJhnmwSmFsLJdVJ9vauWuXF3ixiP_E_QkDWjl2794VJl5thTPR5uulbwSA4y7qOW32XXC4t3ZXhBW8wb7XNYWb9Ugm-HGLdfQkeBlBhh_8a_NL2zbuEsPNVdH3jDDjfmssC3JxQPtExrVTp4lkWtHaxUVF16AR3GJVRUrdLAvdrp0ieOo7OUuSS_KK5T6XvwWLMf0pfVEIvnwesgFLqlXom8We5zxjOIyyJfvyj0TNM_TM9iUKimPsNTyNl3-toIAw6A8_5w4vCc538UVIYOBDvpiunZMFNmdZj7_d6wy__om_MdbiDjmuFxsyvl1jLg6GE_hNrD557fH75noCIAwZ3x9WHHgkvm2-tawBf6Ls3QVXIut2d6kx4sIunZwOhOQLH19kR5N7B-L5FfSTlQrjydXwSufxEkmXtrBc__CfApXmlGQR3LotLBB26tQeIOhIWxbLt14IHlQ-3pnmlBhiq-bteKBLbcIcdGIcZgvNhGSn8ngR-nzWxd-9N2erY49A8D8hOUYk3Dt8_ZiQDnDHDdkNgOsW2KJhC2CROtE5HlcKY3gUOnaye5k2JjAmTKNjSkEvfQRDSitse1nTs0C99EQgUzmGG1taUOLmZy-AEviO3jj3g4o4TwSe0CgV8f4XlHLxbaMjchemOb8";

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