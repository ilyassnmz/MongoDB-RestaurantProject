using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.AdminLayoutComponents
{
    public class _AdminLayoutNavbarViewComponent(IAdminService adminService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = HttpContext.Session.GetString("UserName").ToString();

            var admin = await adminService.GetAdminByUserNameAsync(userName);

            ViewBag.fullName = string.Join(" ", admin.FirstName, admin.LastName);
            return View();
        }
    }
}