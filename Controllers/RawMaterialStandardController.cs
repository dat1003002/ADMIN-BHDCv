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
  public class RawMaterialStandardController : Controller
  {
    private readonly IRawMaterialStandardService _rawMaterialStandardService;
    private const int CategoryId = 19; // Raw Material Standard category
    private const int PageSize = 9;

    public RawMaterialStandardController(IRawMaterialStandardService rawMaterialStandardService)
    {
      _rawMaterialStandardService = rawMaterialStandardService;
    }

    // GET: /RawMaterialStandard/RawMaterialStandard
    public async Task<IActionResult> RawMaterialStandard(int page = 1)
    {
      var products = await _rawMaterialStandardService.GetProducts(CategoryId, page, PageSize);
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ListRawMaterialStandard.cshtml", products);
    }

    // GET: /RawMaterialStandard/SearchRawMaterialStandard
    public async Task<IActionResult> SearchRawMaterialStandard(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(nameof(RawMaterialStandard));
      }
      var products = await _rawMaterialStandardService.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ListRawMaterialStandard.cshtml", products);
    }

    // GET: /RawMaterialStandard/CreateRawMaterialStandard
    public async Task<IActionResult> CreateRawMaterialStandard()
    {
      var categories = await _rawMaterialStandardService.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateRawMaterialStandard.cshtml");
    }

    // POST: /RawMaterialStandard/CreateRawMaterialStandard
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRawMaterialStandard(RawMaterialStandardDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _rawMaterialStandardService.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/CreateRawMaterialStandard.cshtml", product);
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

      await _rawMaterialStandardService.AddProductAsync(product);
      return RedirectToAction(nameof(RawMaterialStandard));
    }

    // GET: /RawMaterialStandard/EditRawMaterialStandard
    public async Task<IActionResult> EditRawMaterialStandard(int id)
    {
      var product = await _rawMaterialStandardService.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      var categories = await _rawMaterialStandardService.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/EditRawMaterialStandard.cshtml", product);
    }

    // POST: /RawMaterialStandard/EditRawMaterialStandard
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRawMaterialStandard(RawMaterialStandardDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _rawMaterialStandardService.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/EditRawMaterialStandard.cshtml", product);
      }

      var existingProduct = await _rawMaterialStandardService.GetProductByIdAsync(product.ProductId);
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

      await _rawMaterialStandardService.UpdateProductAsync(product);
      return RedirectToAction(nameof(RawMaterialStandard));
    }

    // POST: /RawMaterialStandard/DeleteRawMaterialStandard
    [HttpPost]
    public async Task<IActionResult> DeleteRawMaterialStandard(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }
      await _rawMaterialStandardService.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Hướng dẫn đã được xóa thành công!" });
    }

    // GET: /RawMaterialStandard/ShowRawMaterialStandard
    public async Task<IActionResult> ShowRawMaterialStandard(int id)
    {
      var product = await _rawMaterialStandardService.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/ProductQC/ShowRawMaterialStandard.cshtml", product);
    }
  }
}
