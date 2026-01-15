using System.Threading.Tasks;
using Medinova.Contexts;
using Medinova.Helpers;
using Medinova.Models;
using Medinova.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medinova.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envoriment;
        private readonly string _folderPath;

        public DoctorController(AppDbContext context, IWebHostEnvironment envoriment)
        {
            _context = context;
            _envoriment = envoriment;
            _folderPath = Path.Combine(_envoriment.WebRootPath,"assets","img");
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.Select(x => new DoctorGetVM()
            {
                FullName = x.FullName,
                ImagePath = x.ImagePath,
                Id = x.Id

            }).ToListAsync();


            return View(doctors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
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

            var uniqueDoctorImg = await vm.Image.FileUploadAsync(_folderPath);


            Doctor doctor = new()
            {
                FullName = vm.FullName,
                ImagePath = uniqueDoctorImg
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteDoctor = await _context.Doctors.FindAsync(id);

            if (deleteDoctor is null)
            {
                return BadRequest();
            }

            string deletedDoctorImg = Path.Combine(_folderPath, deleteDoctor.ImagePath);

            ExtensionMethods.DeleteFile(deletedDoctorImg);

            _context.Doctors.Remove(deleteDoctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var updateDoctor = await _context.Doctors.FindAsync(id);

            if (updateDoctor is null)
            {
                return BadRequest();
            }

            DoctorUpdateVM vm = new()
            {
                FullName = updateDoctor.FullName
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DoctorUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
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

            var updatedDoctor = await _context.Doctors.FindAsync(vm.Id);

            if (updatedDoctor == null)
            {
                return BadRequest();
            }

            updatedDoctor.FullName = vm.FullName;

            if (vm.Image != null)
            {
                string oldImagePath = Path.Combine(_folderPath, updatedDoctor.ImagePath);
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
                ExtensionMethods.DeleteFile(oldImagePath);
                updatedDoctor.ImagePath = newImagePath;
            }

            _context.Doctors.Update(updatedDoctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
