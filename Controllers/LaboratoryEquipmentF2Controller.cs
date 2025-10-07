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
  public class LaboratoryEquipmentF2Controller : Controller
  {
    private readonly ILaboratoryEquipmentF2Service _laboratoryEquipmentF2Service;
    private const int CategoryId = 18; // Laboratory Equipment F#2 category
    private const int PageSize = 9;

    public LaboratoryEquipmentF2Controller(ILaboratoryEquipmentF2Service laboratoryEquipmentF2Service)
    {
      _laboratoryEquipmentF2Service = laboratoryEquipmentF2Service;
    }

    // GET: /LaboratoryEquipmentF2/LaboratoryEquipmentF2
    public async Task<IActionResult> LaboratoryEquipmentF2(int page = 1)
    {
      var products = await _laboratoryEquipmentF2Service.GetProducts(CategoryId, page, PageSize);
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ListLaboratoryEquipmentF2.cshtml", products);
    }

    // GET: /LaboratoryEquipmentF2/SearchLaboratoryEquipmentF2
    public async Task<IActionResult> SearchLaboratoryEquipmentF2(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(nameof(LaboratoryEquipmentF2));
      }
      var products = await _laboratoryEquipmentF2Service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ListLaboratoryEquipmentF2.cshtml", products);
    }

    // GET: /LaboratoryEquipmentF2/CreateLaboratoryEquipmentF2
    public async Task<IActionResult> CreateLaboratoryEquipmentF2()
    {
      var categories = await _laboratoryEquipmentF2Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateLaboratoryEquipmentF2.cshtml");
    }

    // POST: /LaboratoryEquipmentF2/CreateLaboratoryEquipmentF2
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateLaboratoryEquipmentF2(LaboratoryEquipmentF2DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _laboratoryEquipmentF2Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/CreateLaboratoryEquipmentF2.cshtml", product);
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

      await _laboratoryEquipmentF2Service.AddProductAsync(product);
      return RedirectToAction(nameof(LaboratoryEquipmentF2));
    }

    // GET: /LaboratoryEquipmentF2/EditLaboratoryEquipmentF2
    public async Task<IActionResult> EditLaboratoryEquipmentF2(int id)
    {
      var product = await _laboratoryEquipmentF2Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      var categories = await _laboratoryEquipmentF2Service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/EditLaboratoryEquipmentF2.cshtml", product);
    }

    // POST: /LaboratoryEquipmentF2/EditLaboratoryEquipmentF2
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLaboratoryEquipmentF2(LaboratoryEquipmentF2DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _laboratoryEquipmentF2Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/EditLaboratoryEquipmentF2.cshtml", product);
      }

      var existingProduct = await _laboratoryEquipmentF2Service.GetProductByIdAsync(product.ProductId);
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

      await _laboratoryEquipmentF2Service.UpdateProductAsync(product);
      return RedirectToAction(nameof(LaboratoryEquipmentF2));
    }

    // POST: /LaboratoryEquipmentF2/DeleteLaboratoryEquipmentF2
    [HttpPost]
    public async Task<IActionResult> DeleteLaboratoryEquipmentF2(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }
      await _laboratoryEquipmentF2Service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Hướng dẫn đã được xóa thành công!" });
    }

    // GET: /LaboratoryEquipmentF2/ShowLaboratoryEquipmentF2
    public async Task<IActionResult> ShowLaboratoryEquipmentF2(int id)
    {
      var product = await _laboratoryEquipmentF2Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/ProductQC/ShowLaboratoryEquipmentF2.cshtml", product);
    }
  }
}
