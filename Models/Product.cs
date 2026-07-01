using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mini_store.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم المنتج مطلوب")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "السعر مطلوب")]
    [Range(1, 1000000, ErrorMessage = "السعر لازم يكون أكبر من صفر")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "الوصف مطلوب")]
    public string? Description { get; set; }

    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    [Required(ErrorMessage = "اختر الفئة")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}