using AkademiQMongoDb.Areas.Admin.ViewModels;
using AkademiQMongoDb.Services.EmailServices;
using AkademiQMongoDb.Services.SubscriberServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DiscountEmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ISubscriberService _subscriberService;

        public DiscountEmailController(
            IEmailService emailService,
            ISubscriberService subscriberService)
        {
            _emailService = emailService;
            _subscriberService = subscriberService;
        }

        public async Task<IActionResult> Index()
        {
            var subscribers = await _subscriberService.GetAllAsync();
            var activeSubscribers = await _subscriberService.GetActiveSubscribersAsync();

            ViewBag.TotalSubscribers = subscribers.Count;
            ViewBag.ActiveSubscribers = activeSubscribers.Count;

            return View(new DiscountEmailViewModel()); // Burada View döndürüyor
        }

        [HttpPost]
        public async Task<IActionResult> SendDiscountEmail(DiscountEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                // Aboneleri getir
                var subscribers = model.SendToActiveOnly
                    ? await _subscriberService.GetActiveSubscribersAsync()
                    : await _subscriberService.GetAllAsync();

                if (!subscribers.Any())
                {
                    TempData["Error"] = "Gönderilecek abone bulunamadı.";
                    return RedirectToAction("Index");
                }

                // Toplu mail gönder
                await _emailService.SendDiscountEmailToSubscribersAsync(
                    model.DiscountCode,
                    model.DiscountPercentage
                );

                TempData["Success"] = $"{subscribers.Count} aboneye indirim maili başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Mail gönderilirken hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}