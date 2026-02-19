using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
