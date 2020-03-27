using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace RockStar_IT_Events
{
    public class GetEvents
    {
        string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiZTcyZWZkYjYzZmEzYTRmMjI0ODdkNzQyMDNkMTgzNDkxYzU0MjhhMmE4NzA2M2FlNzA0ZTc3OGQ2OWI5NmFiYzQ0MDkyYzU2NWY0NGE3YmMiLCJpYXQiOjE1ODUxMzY1MzAsIm5iZiI6MTU4NTEzNjUzMCwiZXhwIjoxNjE2NjcyNTMwLCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.LSDlyWT5NIUHNNeZD0yBoZd6Guco2ttb_lqav89wihfTllRX24CODjQP2CNv_QdRjyEqN2qqAGAGh4sj4LzYLgYNzyvOJd6Rgw9lIt5yYKvzKBA3oXTHx1QQ6PXmfErrxfHsgiXV12zkoNzhQOnAPum94tybMn86u5rUEd1fqn-J68Ss8MkpmkIgT2g7EZ7AoVWn2xrOnomACYYManaKYqeZ1WENgIlpEa_v0wwyc5DViJR2mI-5TH2Hekjat01sdwiZJ7Pau86cS397XfJIE64JtJUoPAs3_qzy9U3fZZgoyNKpiXkISVEMh2-U7ITej-qceP8TBCTss90l79D22WKt9YJZdJ5YqmdOiTIjevOHCsLUwtiovcx3HgxXZGBRyDYSpALyVyJELB-0SVRMD_cgbjbhPeOi8qvuyED3gdZnn-G-pYG4PwN9xWFvFeHwOvnrmQoqSSujvLTCwfjQrf8mejc3VnmmyzHzi7_H0kTbD7_qO7zQhgywnIjQ1x2cd5Fqa0F7Z9HvhyVThvNa6GIwt6LgJDSy-LGK4e_P1gY7epCk5gEqXSsH49YriGpjxr_i-xn3GU4Jaf-RF4Y92djJw2JqRIf6Cb37oEjAYCKtI-2VRgnUgjYFmpF74SSyNHP3iKysYWGkvLbBWeLupnbcGcp00hIBANNZqy52jlo";

        WebRequest request = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events");
        request.Headers.Add("Authorization", token);
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        String responseString = reader.ReadToEnd();
        Event event = JsonConvert.DeserializeObject<Event>(json);
        Console.WriteLine(responseString);
        response.Close();
    }
}
