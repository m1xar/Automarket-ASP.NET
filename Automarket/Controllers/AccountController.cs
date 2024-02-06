using Automarket.Domain.ViewModels;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Automarket.Domain.Models;
using Automarket.Domain.Enum;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Automarket.Domain.Response;



namespace Automarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var user = new UserViewModel();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            if (ModelState.ErrorCount < 2)
            {
                var response = await _userService.Verify(user);
                if (response.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    return View(user);
                }
                else
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, response.Data.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, response.Data.Role.ToString())
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return RedirectToAction("GetCars", "Car");
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            var user = new UserViewModel();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Create(user);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Login");
                }
                return View(user);
            }
            return View(user);
        }

        public async Task<IActionResult> ManageUsers()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var response = await _userService.GetUsers();
                var users = response.Data.ToList();
                return View(users);
            }
            return RedirectToAction("AccessDenied", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = new UserViewModel();
            var response = await _userService.GetUserById(id);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                user.Id = response.Data.Id;
                user.Name = response.Data.Name;
                user.Email = response.Data.Email;
                user.Role = response.Data.Role;
                user.Password = response.Data.Password;
                user.CartItems = response.Data.CartItems;
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            await _userService.Edit(user);
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> Create()
        {
            var user = new UserViewModel();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            await _userService.Create(user);
            return RedirectToAction("ManageUsers");
        }
    }
}
