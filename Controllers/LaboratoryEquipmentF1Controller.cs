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
  public class LaboratoryEquipmentF1Controller : Controller
  {
    private readonly ILaboratoryEquipmentF1Service _laboratoryEquipmentF1Service;
    private const int CategoryId = 17; // Laboratory Equipment F#1 category
    private const int PageSize = 9;

    public LaboratoryEquipmentF1Controller(ILaboratoryEquipmentF1Service laboratoryEquipmentF1Service)
    {
      _laboratoryEquipmentF1Service = laboratoryEquipmentF1Service;
    }

    // GET: /LaboratoryEquipmentF1/LaboratoryEquipmentF1
    public async Task<IActionResult> LaboratoryEquipmentF1(int page = 1)
    {
      var products = await _laboratoryEquipmentF1Service.GetProducts(CategoryId, page, PageSize);
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ListLaboratoryEquipmentF1.cshtml", products);
    }

    // GET: /LaboratoryEquipmentF1/SearchLaboratoryEquipmentF1
    public async Task<IActionResult> SearchLaboratoryEquipmentF1(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(nameof(LaboratoryEquipmentF1));
      }
      var products = await _laboratoryEquipmentF1Service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ListLaboratoryEquipmentF1.cshtml", products);
    }

    // GET: /LaboratoryEquipmentF1/CreateLaboratoryEquipmentF1
    public async Task<IActionResult> CreateLaboratoryEquipmentF1()
    {
      var categories = await _laboratoryEquipmentF1Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateLaboratoryEquipmentF1.cshtml");
    }

    // POST: /LaboratoryEquipmentF1/CreateLaboratoryEquipmentF1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateLaboratoryEquipmentF1(LaboratoryEquipmentF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _laboratoryEquipmentF1Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/CreateLaboratoryEquipmentF1.cshtml", product);
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

      await _laboratoryEquipmentF1Service.AddProductAsync(product);
      return RedirectToAction(nameof(LaboratoryEquipmentF1));
    }

    // GET: /LaboratoryEquipmentF1/EditLaboratoryEquipmentF1
    public async Task<IActionResult> EditLaboratoryEquipmentF1(int id)
    {
      var product = await _laboratoryEquipmentF1Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      var categories = await _laboratoryEquipmentF1Service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/EditLaboratoryEquipmentF1.cshtml", product);
    }

    // POST: /LaboratoryEquipmentF1/EditLaboratoryEquipmentF1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLaboratoryEquipmentF1(LaboratoryEquipmentF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _laboratoryEquipmentF1Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/EditLaboratoryEquipmentF1.cshtml", product);
      }

      var existingProduct = await _laboratoryEquipmentF1Service.GetProductByIdAsync(product.ProductId);
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

      await _laboratoryEquipmentF1Service.UpdateProductAsync(product);
      return RedirectToAction(nameof(LaboratoryEquipmentF1));
    }

    // POST: /LaboratoryEquipmentF1/DeleteLaboratoryEquipmentF1
    [HttpPost]
    public async Task<IActionResult> DeleteLaboratoryEquipmentF1(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }
      await _laboratoryEquipmentF1Service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Hướng dẫn đã được xóa thành công!" });
    }

    // GET: /LaboratoryEquipmentF1/ShowLaboratoryEquipmentF1
    public async Task<IActionResult> ShowLaboratoryEquipmentF1(int id)
    {
      var product = await _laboratoryEquipmentF1Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/ProductQC/ShowLaboratoryEquipmentF1.cshtml", product);
    }
  }
}
