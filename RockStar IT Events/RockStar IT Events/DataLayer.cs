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
            //var json = JsonConvert.SerializeObject(e);
            //var data = new StringContent(json,Encoding.UTF8,"application/json");

            //var url = "https://eventhandler-api.herokuapp.com/api/events";
            //using var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNzU4NjhlNDZlMWQxYTY5ZjM3ODJlY2FmYzI1YWU0OTQwZjNmNjEzNmIzMzJiMGE0YTJkNDhiMzEzODM0M2E4NmFlMjBhNmU2ZDk3ZDJlYWQiLCJpYXQiOjE1ODcxMTMxMDcsIm5iZiI6MTU4NzExMzEwNywiZXhwIjoxNjE4NjQ5MTA3LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.SQjBru_we0_PD0a3gKWIXOsbjWriDrv3TaocP-8EbrNCOznqKojPrLtxPubbgnNc1jeuvKYEugSiax6-R142eEuLYaLZCr7M_-ZoT4jWBCCm2B4B-XLDprQOqZx2K-JF_wWjaeNCLeOxxTMgvbjMqLMZ10kKa8JHhk9gYfZdNJkMdcvu_qWYvddKiVvdiqdoYYo2ewgZFeBo_N0CxIZMehGXPZt1csKfejGicEjHfAnb-xHLV9pEjPV7ppOO6liOp1IsYmjIKD6vEOYYx0c4rfgmcTuJAID37u8AFr3mI4Pf5GiNmsbEDgt99Nk1qJ8xUnZzH-WYjNG11GqwRSsy-ng-4R1TdxVRHyaLRg2mgUXl0GCDVmU4kRxlTYCqvWPhej0hUl__K7N7qDISlXG5P3871qhrVznvaLiB7RefQnYCVsq096iwmS6OkUWFOt-7-UgPuLKg0uazhY05copV3Ago18gBdedMywlaDEGWnknaYmApi-mUluqNXCtZIbO04DhLBbf0TMhnsr5X1QfjMyLrPlVzF03Do7h-eaeqf_cLp6G1RI-I1A7EDKTH_hRClWSiisyitWVpb-f6BBbP2c5F_XpfZaaE7ZoL-mFByzc4AnW8oBg8Zt0RzMsLHxQietDGCksyWxiHA1nUrfhvkPMvP0cU7CzMFv-YR7LGfb0");

            //var response = await client.PostAsync(url, data);

            //string result = response.Content.ReadAsStringAsync().Result;

            var json = JsonConvert.SerializeObject(e);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/events";
            var client = new HttpClient();

            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMmQ2NjYyZDMxN2I3OTBkZmY5MjEyNGM5ZTZlZGIxMTZiYzljMGQyNjJmODQ0YjU0NzIwMGQ1OTQ1ZWQ0MTFhMGZiZmJhNTk5YzI4M2Q0MGUiLCJpYXQiOjE1ODc1NDUxNjQsIm5iZiI6MTU4NzU0NTE2NCwiZXhwIjoxNjE5MDgxMTY0LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.g6IJif5fqcINC5 - Q8OF8VoqqrGMt - TzO7iYKxiTMsCWjTUwBuwyJi1lsvb0Y0SBcVypf6TW49QfhPvKyMSk6PIbUrHxzv3Q_VRO1vAc_DQSmGLKofNNp8HW - PnNY6782Nh6ruznHTAPeNpVrAd9CAqcl67MqQj9fS8IzTTabIaftjBCzqJMa2tlMact3mUsmeebdVvNzcOeYyL3Kxqg6AYPOwyHhlHuoG14a_z0qz6QI8mXz4vsDeXVH6IlZOgNScnpFe_8G - oIgfKtR33Ss7YSQvHuqnWyHfiDLwFwYRblINRngdQJP8KoyGqElqvaczw9VAniEUVbICnaiIAFGFb_LCPWSmHKi63yWkX8BJkY7Lk40UfvWCGKCcWm86PmCOjVFoeVf - eAT9 - 59eTL62OMxj7FejhsiJbRnnOlTOt5m8vlmKN2qNte0jOwVRFb--OvBaaJB0drGWuvXj8zYX9zHkvPD1Soi7ky1rT66XBDv07Xa5p5_Qw23LgijZSZr7kqrlPBFA - E2DcVz91X5XMU9jrJR5UC - pRB2PkgjEUMG - SRJyVnYBwmDKjOZo1roeK918hlrFyOz4U4QLGJcCiYpBXnKSdxbkpcltytCCQ4oQqXqLljW6JzInN086n4hGdWPAElB5wygeMDUrx3LkkFbITKdtrwXWTHfgHANiAk");
            //var response = await client.GetStringAsync(url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMmQ2NjYyZDMxN2I3OTBkZmY5MjEyNGM5ZTZlZGIxMTZiYzljMGQyNjJmODQ0YjU0NzIwMGQ1OTQ1ZWQ0MTFhMGZiZmJhNTk5YzI4M2Q0MGUiLCJpYXQiOjE1ODc1NDUxNjQsIm5iZiI6MTU4NzU0NTE2NCwiZXhwIjoxNjE5MDgxMTY0LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.g6IJif5fqcINC5-Q8OF8VoqqrGMt-TzO7iYKxiTMsCWjTUwBuwyJi1lsvb0Y0SBcVypf6TW49QfhPvKyMSk6PIbUrHxzv3Q_VRO1vAc_DQSmGLKofNNp8HW-PnNY6782Nh6ruznHTAPeNpVrAd9CAqcl67MqQj9fS8IzTTabIaftjBCzqJMa2tlMact3mUsmeebdVvNzcOeYyL3Kxqg6AYPOwyHhlHuoG14a_z0qz6QI8mXz4vsDeXVH6IlZOgNScnpFe_8G-oIgfKtR33Ss7YSQvHuqnWyHfiDLwFwYRblINRngdQJP8KoyGqElqvaczw9VAniEUVbICnaiIAFGFb_LCPWSmHKi63yWkX8BJkY7Lk40UfvWCGKCcWm86PmCOjVFoeVf-eAT9-59eTL62OMxj7FejhsiJbRnnOlTOt5m8vlmKN2qNte0jOwVRFb--OvBaaJB0drGWuvXj8zYX9zHkvPD1Soi7ky1rT66XBDv07Xa5p5_Qw23LgijZSZr7kqrlPBFA-E2DcVz91X5XMU9jrJR5UC-pRB2PkgjEUMG-SRJyVnYBwmDKjOZo1roeK918hlrFyOz4U4QLGJcCiYpBXnKSdxbkpcltytCCQ4oQqXqLljW6JzInN086n4hGdWPAElB5wygeMDUrx3LkkFbITKdtrwXWTHfgHANiAk");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
<<<<<<< HEAD
=======
            
            Console.WriteLine(result);
>>>>>>> 0a075f3432e66f7e18b2e40ae9146b2c98984ae2
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
