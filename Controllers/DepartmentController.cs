using KpiNew.Dto;
using KpiNew.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KpiNew.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController( IDepartmentService departmentService)
        {
            _departmentService = departmentService;   
        }
        

        //  [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var department = await _departmentService.GetAllDepartment();
            if (department == null)
            {
                return NotFound();
            }
            return View(department.Data);
        }

        //  [Authorize(Roles ="Admin")]        
        public IActionResult Create()
        {
            return View();  
        }
        // [Authorize(Roles ="Admin")] 
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentRequestModel model)
        {
            await _departmentService.AddDepartment(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
         [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department.Data == null)
            {
                return ViewBag.Message("Department not found");
            }
            return View(department.Data);
        }

        [HttpGet]
         [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateDepartmentRequestModel model)
        {
            await _departmentService.UpdateDepartment(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
         [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {

            var department = await _departmentService.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _departmentService.DeleteDepartment(id);
            return RedirectToAction("Index");
        }

    }
}
