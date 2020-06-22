using Newtonsoft.Json;
using Rockstar.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Data
{
    public class HostApi
    {
        private HttpClient client;

        public HostApi(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<Host> GetHost(string hostId)
        {
            using (HttpResponseMessage response = await client.GetAsync($"hosts/{hostId}"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Host>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<List<Host>> GetAllHosts()
        {
            using (HttpResponseMessage response = await client.GetAsync("hosts"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Host>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task CreateHost(Host host, string bearerToken)
        {
            var json = JsonConvert.SerializeObject(host);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PostAsync("hosts", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task UpdateHost(Host updatedHost, string bearerToken)
        {
            var json = JsonConvert.SerializeObject(updatedHost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PutAsync($"hosts/{updatedHost.id}", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task DeleteHost(string hostId, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.DeleteAsync($"hosts/{hostId}"))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<List<Host>> GetFollowingHosts(string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.GetAsync("users/following/hosts"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Host>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task FollowHost(string hostId, string bearerToken)
        {
            var jsonHostContainer = new Dictionary<string, string>
            {
                { "host_id", hostId}
            };

            var json = JsonConvert.SerializeObject(jsonHostContainer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PostAsync("hosts/follow", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task UnfollowHost(string hostId, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage reponse = await client.DeleteAsync($"hosts/unfollow/{hostId}"))
            {
                if(reponse.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(reponse.ReasonPhrase);
            }
        }
    }
}