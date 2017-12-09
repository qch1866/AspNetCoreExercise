using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SocialAuthentication.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        public AccountController( ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Forbidden()
        //{
        //    return Forbid();
        //}

        [HttpGet]
        public IActionResult SignInWithGoogle()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("HandleExternalLogin", "Account")
            };

            return Challenge(authenticationProperties, "Google"); // The name of the google authentication middleware
        }

        [HttpGet]
        public async Task<IActionResult> HandleExternalLogin()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("ExternalCookie");

            //do something the the claimsPrincipal, possibly create a new one with additional information
            //create a local user, etc

            await HttpContext.SignInAsync("ApplicationCookie", authenticateResult.Principal);
            await HttpContext.SignOutAsync("ExternalCookie");
            var user = HttpContext.User;
            return Redirect("/");
        }
    }
}