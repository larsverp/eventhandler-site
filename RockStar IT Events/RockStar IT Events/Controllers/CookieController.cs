﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.Models;
using Newtonsoft.Json;

namespace RockStar_IT_Events.Controllers
{
    public class CookieController : Controller
    {
        private readonly string _key = ".rockstar_Auth";


        public async Task CreateCookie(string accessToken)
        {

            string _value = accessToken;

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(10);
            cookieOptions.HttpOnly = true;

            Response.Cookies.Append(_key, _value, cookieOptions);
        }

        public async Task ReadCookie(string username, string password)
        {
            if (Request.Cookies[_key] != null)
            {
                //user authentication.
            }
            else
            {
                var getToken = await GetAccessToken(username, password); 
                await CreateCookie(getToken);
            }
        }

        public IActionResult RemoveCookie()
        {
            if (Request.Cookies[_key] != null)
            {
                Response.Cookies.Delete(_key);
            }
            return RedirectToAction("Index", "Event");
        }


        public async Task<string> GetAccessToken(string username, string password)
        {

            var userDetails = new Dictionary<string, string>()
            {
                { "username", username },
                { "password", password }
            };

            string json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://eventhandler-api.herokuapp.com/api/users/login";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
<<<<<<< HEAD

=======
            
>>>>>>> develop
            return JsonConvert.DeserializeObject<Token>(result).access_token;
        }
    }
}