using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class ProductStandardController : Controller
  {
    private readonly IProductStandardService _productStandardService;
    private const int CategoryId = 15; // Product Standard category
    private const int PageSize = 9;

    public ProductStandardController(IProductStandardService productStandardService)
    {
      _productStandardService = productStandardService;
    }

    // GET: /ProductStandard/ProductStandard
    public async Task<IActionResult> ProductStandard(int page = 1)
    {
      var products = await _productStandardService.GetProducts(CategoryId, page, PageSize);
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ListProductStandard.cshtml", products);
    }

    // GET: /ProductStandard/SearchProductStandard
    public async Task<IActionResult> SearchProductStandard(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(nameof(ProductStandard));
      }
      var products = await _productStandardService.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ListProductStandard.cshtml", products);
    }

    // GET: /ProductStandard/CreateProductStandard
    public async Task<IActionResult> CreateProductStandard()
    {
      var categories = await _productStandardService.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateProductStandard.cshtml");
    }

    // POST: /ProductStandard/CreateProductStandard
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProductStandard(ProductStandardDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _productStandardService.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/CreateProductStandard.cshtml", product);
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }
        var filePath = Path.Combine(directoryPath, fileName);
        if (System.IO.File.Exists(filePath))
        {
          string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, newFileName);
          product.image = newFileName;
        }
        else
        {
          product.image = fileName;
        }
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }

      await _productStandardService.AddProductAsync(product);
      return RedirectToAction(nameof(ProductStandard));
    }

    // GET: /ProductStandard/EditProductStandard
    public async Task<IActionResult> EditProductStandard(int id)
    {
      var product = await _productStandardService.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      var categories = await _productStandardService.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/EditProductStandard.cshtml", product);
    }

    // POST: /ProductStandard/EditProductStandard
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProductStandard(ProductStandardDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _productStandardService.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/EditProductStandard.cshtml", product);
      }

      var existingProduct = await _productStandardService.GetProductByIdAsync(product.ProductId);
      if (existingProduct == null)
      {
        return NotFound();
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }
        var filePath = Path.Combine(directoryPath, fileName);
        if (System.IO.File.Exists(filePath))
        {
          string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, newFileName);
          product.image = newFileName;
        }
        else
        {
          product.image = fileName;
        }
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }
      else
      {
        product.image = existingProduct.image;
      }

      await _productStandardService.UpdateProductAsync(product);
      return RedirectToAction(nameof(ProductStandard));
    }

    // POST: /ProductStandard/DeleteProductStandard
    [HttpPost]
    public async Task<IActionResult> DeleteProductStandard(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }
      await _productStandardService.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Hướng dẫn đã được xóa thành công!" });
    }

    // GET: /ProductStandard/ShowProductStandard
    public async Task<IActionResult> ShowProductStandard(int id)
    {
      var product = await _productStandardService.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/ProductQC/ShowProductStandard.cshtml", product);
    }
  }
}
