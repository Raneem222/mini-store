using Microsoft.AspNetCore.Mvc;
using mini_store.Data;
using mini_store.Models;
using Microsoft.AspNetCore.Hosting;

namespace mini_store.Controllers;

public class ProductsController : Controller
{
   private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductsController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }
    // عرض كل المنتجات (Read)
   public IActionResult Index(string searchTerm)
{
    var products = _context.Products.AsQueryable();

    if (!string.IsNullOrEmpty(searchTerm))
    {
        products = products.Where(p => p.Name.Contains(searchTerm));
    }

    ViewBag.CurrentSearch = searchTerm;
    return View(products.ToList());
}

    // عرض صفحة الإضافة (Create - GET)
 public IActionResult Create()
{
    ViewBag.Categories = _context.Categories.ToList();
    return View();
}

    // حفظ المنتج الجديد (Create - POST)
   [HttpPost]
[HttpPost]
public IActionResult Create(Product product)
{
    if (!ModelState.IsValid)
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }

    // رفع الصورة لو المستخدم اختار ملف
    if (product.ImageFile != null)
    {
        string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
        string filePath = Path.Combine(uploadsFolder, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            product.ImageFile.CopyTo(stream);
        }

        product.Image = uniqueName;
    }

    _context.Products.Add(product);
    _context.SaveChanges();
    return RedirectToAction("Index");
}

    // عرض صفحة التعديل (Edit - GET)
   public IActionResult Edit(int id)
{
    var product = _context.Products.Find(id);
    if (product == null) return NotFound();
    ViewBag.Categories = _context.Categories.ToList();
    return View(product);
}

    // حفظ التعديلات (Edit - POST)
  [HttpPost]
public IActionResult Edit(Product product)
{
    if (product.ImageFile != null)
    {
        string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
        string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
        string filePath = Path.Combine(uploadsFolder, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            product.ImageFile.CopyTo(stream);
        }

        product.Image = uniqueName;
    }

    _context.Products.Update(product);
    _context.SaveChanges();
    return RedirectToAction("Index");
}

    // حذف المنتج (Delete)
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}