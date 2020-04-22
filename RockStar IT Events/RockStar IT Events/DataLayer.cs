using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RockStar_IT_Events.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RockStar_IT_Events
{
    public class DataLayer
    {
        private WebRequest webRequest;
        private WebResponse webResponse;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        
        public List<Event> GetAllEvents()
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events");

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(webResponseString);

            return events;
        }


        public Event GetEvent(string id)
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events/" + id);

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string json = streamReader.ReadToEnd();

            Event output = JsonConvert.DeserializeObject<Event>(json);

            return output;
        }

        public async Task Create(Event e, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(e);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/events";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMmQ2NjYyZDMxN2I3OTBkZmY5MjEyNGM5ZTZlZGIxMTZiYzljMGQyNjJmODQ0YjU0NzIwMGQ1OTQ1ZWQ0MTFhMGZiZmJhNTk5YzI4M2Q0MGUiLCJpYXQiOjE1ODc1NDUxNjQsIm5iZiI6MTU4NzU0NTE2NCwiZXhwIjoxNjE5MDgxMTY0LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.g6IJif5fqcINC5-Q8OF8VoqqrGMt-TzO7iYKxiTMsCWjTUwBuwyJi1lsvb0Y0SBcVypf6TW49QfhPvKyMSk6PIbUrHxzv3Q_VRO1vAc_DQSmGLKofNNp8HW-PnNY6782Nh6ruznHTAPeNpVrAd9CAqcl67MqQj9fS8IzTTabIaftjBCzqJMa2tlMact3mUsmeebdVvNzcOeYyL3Kxqg6AYPOwyHhlHuoG14a_z0qz6QI8mXz4vsDeXVH6IlZOgNScnpFe_8G-oIgfKtR33Ss7YSQvHuqnWyHfiDLwFwYRblINRngdQJP8KoyGqElqvaczw9VAniEUVbICnaiIAFGFb_LCPWSmHKi63yWkX8BJkY7Lk40UfvWCGKCcWm86PmCOjVFoeVf-eAT9-59eTL62OMxj7FejhsiJbRnnOlTOt5m8vlmKN2qNte0jOwVRFb--OvBaaJB0drGWuvXj8zYX9zHkvPD1Soi7ky1rT66XBDv07Xa5p5_Qw23LgijZSZr7kqrlPBFA-E2DcVz91X5XMU9jrJR5UC-pRB2PkgjEUMG-SRJyVnYBwmDKjOZo1roeK918hlrFyOz4U4QLGJcCiYpBXnKSdxbkpcltytCCQ4oQqXqLljW6JzInN086n4hGdWPAElB5wygeMDUrx3LkkFbITKdtrwXWTHfgHANiAk");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> GetBearerToken(string username, string password)
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

            

            var r = await client.PostAsync(url, data);

            return "sa";

            string result = r.Content.ReadAsStringAsync().Result;
            string token = JsonConvert.DeserializeObject<Token>(result).access_token;
            return token;
        }

        public class Token
        {
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
        }
    }
}
