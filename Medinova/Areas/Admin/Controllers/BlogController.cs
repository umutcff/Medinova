using System.Threading.Tasks;
using Medinova.Contexts;
using Medinova.Helpers;
using Medinova.Models;
using Medinova.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Medinova.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envoriment;
        private string _folderPath;

        public BlogController(AppDbContext context, IWebHostEnvironment envoriment)
        {
            _context = context;
            _envoriment = envoriment;
            _folderPath = Path.Combine(_envoriment.WebRootPath, "assets", "img");
        }

        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.Include(x => x.Doctor).Select(x=>new BlogGetVM
            {
                Id=x.Id,
                DoctorName=x.Doctor.FullName,
                Description=x.Description,
                ImagePath=x.ImagePath,
                Tittle=x.Tittle

            }).ToListAsync();

            return View(blog);
        }

        public async Task<IActionResult> Create()
        {
            await sendDoctorViewBag();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM vm)
        {
            await sendDoctorViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var isExistDoctor = await _context.Doctors.AnyAsync(x => x.Id == vm.DoctorId);

            if (!isExistDoctor)
            {
                ModelState.AddModelError("DoctorId", "Yoxdur bele DoctorId");
                return View(vm);
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "This file must be fewer than 2mb");
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "This file must be type of Image");
                return View(vm);
            }

           var uniqueImageName=await vm.Image.FileUploadAsync(_folderPath);

            Blog blog = new()
            {
                Description=vm.Description,
                Tittle=vm.Tittle,
                DoctorId=vm.DoctorId,
                ImagePath=uniqueImageName,
            };

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteBlog = await _context.Blogs.FindAsync(id);

            if (deleteBlog == null)
            {
                return BadRequest();
            }

            string deletedImagePath = Path.Combine(_folderPath, deleteBlog.ImagePath);

            ExtensionMethods.DeleteFile(deletedImagePath);

            _context.Blogs.Remove(deleteBlog);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            await sendDoctorViewBag();

            var updateBlog = await _context.Blogs.FindAsync(id);

            if (updateBlog == null)
            {
                return BadRequest();
            }

            BlogUpdateVM vm = new()
            {
                Id=updateBlog.Id,
                Tittle=updateBlog.Tittle,
                Description=updateBlog.Description,
                DoctorId=updateBlog.DoctorId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateVM vm)
        {
            await sendDoctorViewBag();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var isUpdateDoctor = await _context.Doctors.AnyAsync(x => x.Id == vm.DoctorId);

            if (!isUpdateDoctor)
            {
                ModelState.AddModelError("DoctorId", "Not found DoctorId");
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("image", "File must be type of image");
                return View(vm);
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("image", "File must be fewer than 2-mb");
                return View(vm);
            }

            var updatedBlog = await _context.Blogs.FindAsync(vm.Id);

            if (updatedBlog == null)
            {
                return BadRequest();
            }

            updatedBlog.Tittle = vm.Tittle;
            updatedBlog.Description = vm.Description;
            updatedBlog.DoctorId = vm.DoctorId;

            if (vm.Image != null)
            {
                string oldImagePath = Path.Combine(_folderPath, updatedBlog.ImagePath);
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
                ExtensionMethods.DeleteFile(oldImagePath);
                updatedBlog.ImagePath = newImagePath;
            }
            _context.Update(updatedBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }











        private async Task sendDoctorViewBag()
        {
            var doctors = await _context.Doctors.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FullName,

            }).ToListAsync();

            ViewBag.Doctors = doctors;
        }
    }
}
