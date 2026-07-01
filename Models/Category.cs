namespace mini_store.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Icon { get; set; }

    // قائمة المنتجات التابعة لهذه الفئة (جسر الربط)
    public List<Product> Products { get; set; } = new();
}