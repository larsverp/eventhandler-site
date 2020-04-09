using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RockStar_IT_Events.Models;

namespace RockStar_IT_Events.Controllers
{
    public class CookieController
    {
        private HttpClient httpClient = new HttpClient();
        private static readonly string _key = ".rockstar_Auth";
        private HttpResponse Response;
        private HttpRequest Request;

        public void CreateCookie(string accessToken)
        {
            string _value = accessToken;

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(10);
            cookieOptions.HttpOnly = true;
            Response.Cookies.Append(_key, _value, cookieOptions);
        }

        public void ReadCookie()
        {
            if (Request.Cookies[_key] != null)
            {
                //Cookie found! Log user in automatically.
            }
            else
            {
                //No cookie found.
                CreateCookie(GetAccessToken().Result);
            }
        }

        public void RemoveCookie()
        {
            if (Request.Cookies[_key] != null)
            {
                Response.Cookies.Delete(_key);
            }
        }

        public async Task<string> GetAccessToken()//string username, string password
        {

            string _username = "test@test.com";
            string _password = "password";

            var userDetails = new Dictionary<string, string>()
            {
                { "username", _username },
                { "password", _password }
            };

            string json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://eventhandler-api.herokuapp.com/api/users/login";

            var response = await httpClient.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Token>(result).access_token;
        }
    }
}