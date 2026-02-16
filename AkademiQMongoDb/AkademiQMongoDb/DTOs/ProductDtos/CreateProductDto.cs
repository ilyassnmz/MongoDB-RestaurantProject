using System.ComponentModel.DataAnnotations;

namespace AkademiQMongoDb.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage="Ürün adı boş bırakılamaz." )]
        [MinLength(3,ErrorMessage = "Ürün adı en az 3 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ürün Açıklaması boş bırakılamaz.")]
        [MaxLength(250,ErrorMessage = "Ürün açıklaması 250 karakteri geçemez.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Görsel Url boş bırakılamaz.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Ürün fiyatı boş bırakılamaz.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Kategori adı boş bırakılamaz.")]
        public string CategoryName { get; set; }
    }
}
