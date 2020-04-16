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
        private string username;
        private string password;

        public CookieController(string _username, string _password)
        {
            username = _username;
            password = _password;
        }
        public void CreateCookie(string accessToken)
        {
            string _value = accessToken;

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(10);
            cookieOptions.HttpOnly = true;

            Response.Cookies.Append(_key, _value, cookieOptions);
        }

        public async void ReadCookie(string username, string password)
        {
            if (Request.Cookies[_key] != null)
            {
                //user authentication.
            }
            else
            {
                //var getToken = GetAccessToken(username, password); 
                //CreateCookie(getToken);
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


        public static async void GetAccessToken(string username, string password)
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

            string retrn = JsonConvert.DeserializeObject<Token>(result).access_token;

            CreateCookie(retrn);
        }
    }
}