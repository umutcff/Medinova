using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Medinova.Contexts;
using Medinova.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medinova.Controllers
{
    public class BlogController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.Select(x => new BlogGetVM()
            {
                Tittle=x.Tittle,
                Description=x.Description,
                DoctorName=x.Doctor.FullName,
                DoctorImagePath = x.Doctor.ImagePath

            }).ToListAsync();
            return View();
        }
    }
}
