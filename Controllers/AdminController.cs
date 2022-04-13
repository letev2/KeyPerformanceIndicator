using KpiNew.Dto;
using KpiNew.Interfaces.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KpiNew.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IAdminService adminService, IUserService userService,
            IRoleService roleService, IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _adminService = adminService;
            _userService = userService;
            _roleService = roleService;
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var admin = await _adminService.GetAllAdmin();
            return View(admin.Data);
        }

        public IActionResult AdminDashBoard()
        {
            
            return View();
        }
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminRequestModel model, IFormFile adminPhoto)
        {
            if (adminPhoto != null)
            {
                string adminPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "adminPhotos");
                Directory.CreateDirectory(adminPhotoPath);
                string contentType = adminPhoto.ContentType.Split('/')[1];
                string adminImage = $"AD{Guid.NewGuid()}.{contentType}";
                string fullPath = Path.Combine(adminPhotoPath, adminImage);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    adminPhoto.CopyTo(fileStream);
                }
                model.adminPhoto = adminImage;



            }
            await _adminService.AddAdmin(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin.Data == null)
            {
                return ViewBag.Message("Admin not found");
            }
            return View(admin.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateAdminRequestModel model)
        {
            await _adminService.UpdateAdmin(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _adminService.DeleteAdmin(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            //int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adminemail = User.FindFirst(ClaimTypes.Email).Value;
            var admins = await _adminService.GetAdminByEmail(adminemail);
             var admin =  await _adminService.GetAdminById(admins.Data.Id);
            return View(admin.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ChartBoard()
        {
       
            var res = await _employeeService.CalculateAllEmployeeRatingYearly();
            
            List<string> FullName = new List<string>();
            List<double> SumTotal = new List<double>();

            foreach (var item in res.Data)
            {
                FullName.Add(item.FullName);
                SumTotal.Add(item.SumTotal);
            }
            var a = FullName;
            var b = SumTotal;
            ViewBag.FULLNAME = a;
            ViewBag.SUMTOTAL = b;

            return View();
            
            

        }


    }






}
