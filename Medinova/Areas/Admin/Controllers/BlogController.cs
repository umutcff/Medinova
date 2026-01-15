using System.Threading.Tasks;
using Medinova.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medinova.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envoriment;
        private string _folderPath;

        public BlogController(AppDbContext context, IWebHostEnvironment envoriment, string folderPath)
        {
            _context = context;
            _envoriment = envoriment;
            _folderPath = Path.Combine(_envoriment.WebRootPath, "assets", "img");
        }

        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.Include(x => x.Doctor).Select(x=>).ToListAsync();

            return View();
        }

        public IActionResult Create()
        {

        }
    }
}
