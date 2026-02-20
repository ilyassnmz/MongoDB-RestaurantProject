using System.ComponentModel.DataAnnotations;

namespace AkademiQMongoDb.Areas.Admin.ViewModels
{
    public class DiscountEmailViewModel
    {
        [Required(ErrorMessage = "İndirim kodu zorunludur")]
        [Display(Name = "İndirim Kodu")]
        public string DiscountCode { get; set; }

        [Required(ErrorMessage = "İndirim yüzdesi zorunludur")]
        [Range(1, 100, ErrorMessage = "İndirim yüzdesi 1-100 arasında olmalıdır")]
        [Display(Name = "İndirim Yüzdesi")]
        public int DiscountPercentage { get; set; }

        [Display(Name = "Sadece aktif abonelere gönder")]
        public bool SendToActiveOnly { get; set; } = true;
    }
}