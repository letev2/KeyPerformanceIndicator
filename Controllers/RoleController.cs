using KpiNew.Dto;
using KpiNew.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace KpiNew.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public RoleController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService; 
        }
        
        public async Task<IActionResult> Index()
        {
            var role = await _roleService.GetAllRole();
            if (role == null)
            {
                return NotFound();
            }
            return View(role.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
             await _roleService.AddRole(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
         
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role.Data == null)
            {
                return ViewBag.Message("User not found");
            }
            return View(role.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateRoleRequestModel model)
        {
            await _roleService.UpdateRole(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {

            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _roleService.DeleteRole(id);
            return RedirectToAction("Index");
        }




    }
}
