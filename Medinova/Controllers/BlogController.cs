using Microsoft.AspNetCore.Mvc;

namespace Medinova.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
