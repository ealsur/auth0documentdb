using auth0documentdb.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace auth0documentdb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOptions<OpenIdConnectOptions> _options;
        public AccountController(IOptions<OpenIdConnectOptions> openIdOptions)
        {
            _options = openIdOptions;
        }
        
        public IActionResult Login(string returnUrl = null)
        {
            var lockContext = HttpContext.GenerateLockContext(_options.Value, returnUrl);
            
            return View(lockContext);
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("Auth0");
            HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }
}
