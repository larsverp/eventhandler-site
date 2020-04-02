using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.Controllers
{
    public class CookieController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly string _key = ".rockstar_Auth";
        public IActionResult CreateCookie(string accessToken)
        {
            string _value = accessToken;

            string value = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNzYyNGMxNTM3YTliMTI0OTVhZGE5ODBiMTI5YTQzZjIwNDJhZDMwMmYyNjdhMjdkOWZmZDllNTAyNTRkNjY2ODQ0NzAwMjU3MGUwZGY0N2YiLCJpYXQiOjE1ODU3MjcwNTQsIm5iZiI6MTU4NTcyNzA1NCwiZXhwIjoxNjE3MjYzMDU0LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.fndSCf0voeKDn5qNslGqwISxK24X6oJRl-BBxAfrHS9S42KUj-v2NgTf0B2F1NhM2Y4TAUwIJQWAN_BeW02lXfMHih8I_oAYp0CLPFKanlcgY_XnSaDJ3xCPo28_7RSJ4-jVAJHdW40SRBz3909JWj6kF4qDKjSx42VYg3EXqxx_pw9wWQyh7jGK9hCqkvTtqiA57Xa84Xv6Xfx9pSOopuSL4WUOvJ-_oKgxfcJ10iC7ojFPwNJysiX_bvOz1CmnIeSFX67EV0kpVFi6WDkEpcyGoQ_4_jALVdWbLLYwT9yDc2l3ugX3LCbjGfnvoSOVi0-yVW9N2O54AhGCzzNibeAP1KES64pqzAcXaVBZHGuWjRKH7JJTlBBWVO8PV8lU4MAjeEzjXIB_7WGpvxTdVdPQPPj4k4l0lulQd0gzVHGw4rzxhyWdyIYwwpHIkUU4MXqe6-zo24nptW2Yh5YhTewZ5drZXvAKm3gHknigoB0jzNVcBUOw6hvBzW_WEh-HDJY5Nmh9Hvm2essMedvIhAIQoeAsXTzKM_A_laX3C89b597C3XzigguCYe6jJWLqCloTyoZPbgjouaRMsCVJjJyPrQF8GrxCNgYbV5ElfJktE4-1OVzXzunOHZ8fBMVUmKlGSX9fv2gkEAvkv0gj71DrlS9ugDsMnaHc3bRgpA8";

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(10);
            cookieOptions.HttpOnly = true;
            Response.Cookies.Append(_key, value, cookieOptions);
            return View();
        }

        public IActionResult ReadCookie()
        {
            if (Request.Cookies[_key] != null)
            {
                //Cookie found! Log user in automatically.
                return View();
            }
            else
            {
                //No cookie found.
                //Ask user if they want to use cookies.
                return View();
            }
            
        }

        public IActionResult RemoveCookie()
        {
            if (Request.Cookies[_key] != null)
            {
                Response.Cookies.Delete(_key);
            }
            return View();
        }

        public IActionResult GetAccessToken()
        {
            
        }
    }
}