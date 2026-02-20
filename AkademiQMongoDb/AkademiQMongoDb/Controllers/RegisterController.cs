using AkademiQMongoDb.DTOs.AdminDtos;
using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
    public class RegisterController(IAdminService _adminService) : Controller
    {
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterAdminDto registerAdminDto)
        {
            await _adminService.CreateAdminAsync(registerAdminDto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, registerAdminDto.Username),
                new Claim(ClaimTypes.NameIdentifier, registerAdminDto.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            HttpContext.Session.SetString("UserName", registerAdminDto.Username);

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}