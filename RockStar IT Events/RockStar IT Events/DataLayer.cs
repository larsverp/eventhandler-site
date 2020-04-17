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

        public async Task Create(Event e)
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
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMDI3MjIzOGY0Y2U0MmQyZjZiMWNhNjI2YmM3OTZiOWUyZmNkY2JkNjEyZjcyYTUyMjUzMzk2ZWNhY2IwMTAxYjc5M2VhNDYxMTZkOGJjYjgiLCJpYXQiOjE1ODcxMTc2NTQsIm5iZiI6MTU4NzExNzY1NCwiZXhwIjoxNjE4NjUzNjU0LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.QOXjtNaFtHsNB9HwHbQZOx0xhSYSn2U2o214dBXzAvW_EF_0Wo-jETkloFhOks3-RPMtAWu6Sbe8pcXSyCReCw63baTivCIjEg1xxQsO1Ej9gFbx3nZr5I3__YRJPa1tMM7nOmKqqAp2XQsxJ7HPS1BzAKvjqULePMzQ0u5XaBH8zCXCMgWb8ZTZd1EKA-a25u1wAzmTLphOhUcE7Z131Df_fbIequWdNQst6l9BdEnwolbyNeQ7QmTcUJRTgLcYV6UsQ7N-CFCbm1YIzkrku6aqSxfDgij-mokkTAjsTdJ9R8xoPog1NXpjnRBgWFPP_bTqffLfpaPSrCOthMC4IOvCNrWgaK79k73wNdYRkHfRE6x-_G0CKjNpWmNks3PA7DC8mVpnDU_Y5cvGaurSh3ng6lilWCtbBuxGQ5H8HP3Dn0HnxoY78SB5Nt2ymM8TRwYAfu8PkbvbNA92gsR5PSqrjmGPMXODlKDkBTc56BGWAEVK11SNyJwn4GkR8aJ_oTEH6FobTLUigd93UHZR0E4nU0MViFN51bMyD-HGnu6hc85GOp2peyFOdu7ZPZaYlAB7KWaku7cUxzX_KQHgy0P7wSduO7tQnknj-_7FH7LVRxvRJdrQ0G8L6uAdMHSdYGgciC3LwjP_1WKswroYjqqxGgzhyW7v9oX3lbAM5YU");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            
            Console.WriteLine(result);
        }
    }
}
