using KpiNew.Dto;
using KpiNew.Interfaces.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KpiNew.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAllUser();
            if (user == null)
            {
                return NotFound();
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user.Data == null)
            {
                return ViewBag.Message("User not found");
            }
            return View(user.Data);
        }


        // [Authorize]
        // public async Task<IActionResult> Profile()
        // {
        //     int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //     var user = await _userService.GetUserById(id);
        //     return View(user.Data);
        // }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            var user = await _userService.Login(model);
            if (user.Data != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Data.FirstName),
                    new Claim(ClaimTypes.GivenName, $"{user.Data.FirstName} {user.Data.LastName}"),
                   new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Data.Email),

                };
                foreach (var role in user.Data.Roles)
                {

                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                foreach (var item in user.Data.Roles)
                {
                    if (item.Name == "Admin")
                    {
                        return RedirectToAction("Profile", "Admin");
                    }

                    if (item.Name == "Employee")
                    {
                        return RedirectToAction("Profile", "Employee");
                    }
                }

                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.error = "Invalid username or password";
                return View();
            }

        }




        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Index");
        }

    }
}