using AkademiQMongoDb.DTOs.AdminDtos;
using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class LoginController(IAdminService _adminService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginAdminDto loginAdminDto)
        {
            var result = await _adminService.LoginAdminAsync(loginAdminDto);
            if(result is false)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı, ya da henüz kaydınız onaylanmamış.");
                return View(loginAdminDto);
            }
            return Redirect("/Admin/Product/Index");
        }
    }
}
