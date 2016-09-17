using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace auth0documentdb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
