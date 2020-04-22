using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.Controllers
{
    public class CookiesController : Controller
    {

        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }

        public string Get(string key)
        {
            return Request.Cookies[key];
        }

        public void Create(string key, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.Secure = true;
            options.HttpOnly = true;

            Response.Cookies.Append(key, value, options);
        }
    }
}