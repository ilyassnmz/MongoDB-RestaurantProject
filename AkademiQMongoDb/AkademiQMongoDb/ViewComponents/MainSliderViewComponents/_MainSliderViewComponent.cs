using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents
{
    public class _MainSliderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}