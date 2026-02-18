using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Layout()
        {
            return View();
        }
    }
}
