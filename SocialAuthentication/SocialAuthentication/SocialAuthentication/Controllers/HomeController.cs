using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SocialAuthentication.Controllers
{
    [Route("[Controller]/[action]")]
    [Authorize(AuthenticationSchemes = "ApplicationCookie")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello Index");
        }

        [HttpGet]
        public IEnumerable<string> GetValue()
        {
            var user = HttpContext.User;
            return new string[] { "value1", "value2" };
        }
    }
}