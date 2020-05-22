using Newtonsoft.Json;
using Rockstar.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Data
{
    public class HostApi
    {
        private WebRequest webRequest;
        private WebResponse webResponse;
        private StreamReader streamReader;

        public Host GetHost(string id)
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/hosts/" + id);

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            Host output = JsonConvert.DeserializeObject<Host>(webResponseString);

            return output;
        }

        public async Task<IEnumerable<Host>> GetAllHosts()
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/hosts");

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            var output = JsonConvert.DeserializeObject<List<Host>>(webResponseString);

            return output;
        }

        public async Task CreateHost(Host host, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(host);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/hosts";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PostAsync(url, data);
                if(response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task UpdateHost(Host updatedHost, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(updatedHost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/hosts/" + updatedHost.id;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PutAsync(url, data);
                if(response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task DeleteHost(string hostId, string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/hosts/" + hostId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }
    }
}