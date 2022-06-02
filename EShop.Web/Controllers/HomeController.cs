using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        
        [Route("/")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
