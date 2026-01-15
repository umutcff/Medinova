using System.Diagnostics;
using Medinova.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medinova.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
