using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class ProductStandardMHEController : Controller
  {
    private readonly IProductStandardMHEService _service;
    private const int CategoryId = 15;
    private const int PageSize = 9;

    public ProductStandardMHEController(IProductStandardMHEService service)
    {
      _service = service;
    }

    public async Task<IActionResult> ListProductStandardMHE(int page = 1)
    {
      var products = await _service.GetProducts(CategoryId, page, PageSize);
      ViewData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ProductStandard/ListProductStandardMHE.cshtml", products);
    }

    public async Task<IActionResult> Search(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
        return RedirectToAction(nameof(ListProductStandardMHE));

      var products = await _service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      ViewData["SearchTerm"] = name;
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ProductStandard/ListProductStandardMHE.cshtml", products);
    }

    public async Task<IActionResult> CreateProductStandardMHE()
    {
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/ProductStandard/CreateProductStandardMHE.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProductStandardMHE(ProductStandardMHEDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/ProductStandard/CreateProductStandardMHE.cshtml", product);
      }

      await HandleImageUpload(product);
      product.CategoryId = CategoryId;
      await _service.AddProductAsync(product);
      return RedirectToAction(nameof(ListProductStandardMHE));
    }

    public async Task<IActionResult> EditProductStandardMHE(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/ProductStandard/EditProductStandardMHE.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProductStandardMHE(ProductStandardMHEDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/ProductStandard/EditProductStandardMHE.cshtml", product);
      }

      var existing = await _service.GetProductByIdAsync(product.ProductId);
      if (existing == null) return NotFound();

      if (product.imageFile != null && product.imageFile.Length > 0)
        await HandleImageUpload(product);
      else
        product.image = existing.image;

      product.CategoryId = CategoryId;
      await _service.UpdateProductAsync(product);
      return RedirectToAction(nameof(ListProductStandardMHE));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int productId)
    {
      await _service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Đã xóa MHE!" });
    }

    public async Task<IActionResult> ShowProductStandardMHE(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/ProductQC/ProductStandard/ShowProductStandardMHE.cshtml", product);
    }

    private async Task HandleImageUpload(ProductStandardMHEDTO product)
    {
      if (product.imageFile == null || product.imageFile.Length == 0) return;

      var fileName = Path.GetFileName(product.imageFile.FileName);
      var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
      Directory.CreateDirectory(dir);
      var filePath = Path.Combine(dir, fileName);

      if (System.IO.File.Exists(filePath))
      {
        fileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        filePath = Path.Combine(dir, fileName);
      }

      product.image = fileName;
      using var stream = new FileStream(filePath, FileMode.Create);
      await product.imageFile.CopyToAsync(stream);
    }
  }
}
