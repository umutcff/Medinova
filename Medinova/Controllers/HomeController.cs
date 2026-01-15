using System.Diagnostics;
using System.Threading.Tasks;
using Medinova.Contexts;
using Medinova.Models;
using Medinova.ViewModels.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medinova.Controllers
{
    //[Authorize]
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var item = await _context.Blogs.Select(x => new BlogGetVM()
            {
                Description=x.Description,
                Tittle=x.Tittle,
                DoctorName=x.Doctor.FullName,
                ImagePath=x.ImagePath

            }).ToListAsync();


            return View(item);
        }
    }
}
