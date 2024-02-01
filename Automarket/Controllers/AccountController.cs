using Automarket.Domain.ViewModels;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Automarket.Domain.Models;
using Automarket.Domain.Enum;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;



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
            var response = await _userService.Verify(user);
            if (response.StatusCode != Domain.Enum.StatusCode.OK) 
            {
                return View(user);
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, response.Data.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, response.Data.Role.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("GetCars", "Car");
            }
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
            var response = await _userService.Create(user);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}
