using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class UILayoutController : Controller
    {
        [AllowAnonymous]
        public IActionResult Layout()
        {
            return View();
        }
    }
}
