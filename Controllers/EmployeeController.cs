using KpiNew.Dto;
using KpiNew.Enum;
using KpiNew.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KpiNew.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService
                       departmentService, IUserService userService, IWebHostEnvironment webHostEnvironment )
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            

        }

       
        public async Task<IActionResult> Index()
        {
            var employee = await _employeeService.GetAllEmployee();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee.Data);
        }
        public async Task<IActionResult> Create()
        {
            var department = await _departmentService.GetAllDepartment();
            ViewData["Department"] = new SelectList(department.Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeRequestModel model, IFormFile employeeImage)
        {

            if (employeeImage != null)
            {
                string employeeImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "EmployeeImage");
                Directory.CreateDirectory(employeeImagePath);
                string contentType = employeeImage.ContentType.Split('/')[1];
                string employeePhoto = $"EM{Guid.NewGuid()}.{contentType}";
                string fullPath = Path.Combine(employeeImagePath, employeePhoto);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    employeeImage.CopyTo(fileStream);
                }
                model.EmployeeImage = employeePhoto;


              await _employeeService.AddEmployee(model);

            }
            return RedirectToAction("Login","User");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee.Data == null)
            {
                return ViewBag.Message("Employee not found");
            }
            return View(employee.Data);
        }

        [HttpGet]
        
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateEmployeeRequestModel model)
        {
            await _employeeService.UpdateEmployee(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        public IActionResult EmployeeDashBoard()
        {
            return View();
        }

        public IActionResult Performance()
        {
         
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> Performance(int id , RatingPerformance model)
        {
            model.EmployeeId = id;
            await _employeeService.Performance(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeByDepartment(int departmentId)
        {
           var employee = await _employeeService.GetEmployeeByDepartment(departmentId);
            return View(employee.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
            var employee = await _employeeService.GetEmployeeByEmail(email);
            return View(employee.Data);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var employeeEmail = User.FindFirst(ClaimTypes.Email).Value;
            var employee = await _employeeService.GetEmployeeByEmail(employeeEmail);
             var employeeId =  await _employeeService.GetEmployeeById(employee.Data.Id);
            return View(employee.Data);
            
        }

      

        [Authorize]
        [HttpGet]
        public IActionResult CalculateEmployeeKpi()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowEmployeeKpiForMonthDetails(EmployeeRatingPerformance model )
        {
            var employeeEmail = User.FindFirst(ClaimTypes.Email).Value;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var calculateEmployee = await _employeeService.CalculateEmployeeKpiForMonth(model,userId);
            return View(calculateEmployee.Data);
        }


        [HttpPost]
        public async Task<IActionResult> ShowEmployeeKpiForYearDetails(EmployeeRatingPerformance model)
        {
            
            var employeeEmail = User.FindFirst(ClaimTypes.Email).Value;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
            
            var calculateEmployeeYear = await _employeeService.CalculateEmployeeKpiForYear(model,userId);
            
            return View(calculateEmployeeYear.Data);

        }



        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeRatingForYear()
        {
            var employee = await _employeeService.CalculateAllEmployeeRatingYearly();
            return View(employee.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeRatingForMonth()
        {
            var employee = await _employeeService.GetAllEmployeeRatingForMonth();
            return View(employee.Data);
        }


    }
}
