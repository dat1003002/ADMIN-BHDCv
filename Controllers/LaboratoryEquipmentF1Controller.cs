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
    private const int PageSize = 9;

    public LaboratoryEquipmentF1Controller(ILaboratoryEquipmentF1Service laboratoryEquipmentF1Service)
    {
      _laboratoryEquipmentF1Service = laboratoryEquipmentF1Service;
    }

    // F#1 - Thẻ Lý Lịch
    public async Task<IActionResult> LaboratoryEquipmentF1(int page = 1)
    {
      int categoryId = 23;
      var products = await _laboratoryEquipmentF1Service.GetProducts(categoryId, page, PageSize);
      ViewBag.CurrentAction = "LaboratoryEquipmentF1";
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ListLaboratoryEquipmentF1.cshtml", products);
    }

    // F#2 - Kết Quả Hiệu Chuẩn
    public async Task<IActionResult> LabEquipF1Ketquahieuchuan(int page = 1)
    {
      int categoryId = 24;
      var products = await _laboratoryEquipmentF1Service.GetProducts(categoryId, page, PageSize);
      ViewBag.CurrentAction = "LabEquipF1Ketquahieuchuan";
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/ListLabEquipF1Ketquahieuchuan.cshtml", products);
    }

    // F#3 - Tiêu Chuẩn Kiểm Tra
    public async Task<IActionResult> LabEquipF1DailyCheck(int page = 1)
    {
      int categoryId = 25;
      var products = await _laboratoryEquipmentF1Service.GetProducts(categoryId, page, PageSize);
      ViewBag.CurrentAction = "LabEquipF1DailyCheck";
      TempData["SearchTerm"] = null;
      return View("~/Views/ProductQC/LabEquipF1DailyCheck.cshtml", products);
    }

    // Tìm kiếm
    public async Task<IActionResult> SearchLaboratoryEquipmentF1(string name, int categoryId = 23, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(categoryId switch
        {
          23 => "LaboratoryEquipmentF1",
          24 => "LabEquipF1Ketquahieuchuan",
          25 => "LabEquipF1DailyCheck",
          _ => "LaboratoryEquipmentF1"
        });
      }

      var products = await _laboratoryEquipmentF1Service.SearchProductsByNameAsync(name, categoryId, page, PageSize);
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");

      ViewBag.CurrentAction = categoryId switch
      {
        23 => "LaboratoryEquipmentF1",
        24 => "LabEquipF1Ketquahieuchuan",
        25 => "LabEquipF1DailyCheck",
        _ => "LaboratoryEquipmentF1"
      };

      return View(categoryId switch
      {
        23 => "~/Views/ProductQC/ListLaboratoryEquipmentF1.cshtml",
        24 => "~/Views/ProductQC/ListLabEquipF1Ketquahieuchuan.cshtml",
        25 => "~/Views/ProductQC/LabEquipF1DailyCheck.cshtml",
        _ => "~/Views/ProductQC/ListLaboratoryEquipmentF1.cshtml"
      }, products);
    }

    // Create
    public async Task<IActionResult> CreateLaboratoryEquipmentF1()
    {
      var categories = await _laboratoryEquipmentF1Service.GetCategories();
      var filtered = categories.Where(c => c.CategoryId == 23 || c.CategoryId == 24 || c.CategoryId == 25).ToList();
      ViewBag.CategoryList = new SelectList(filtered, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateLaboratoryEquipmentF1.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateLaboratoryEquipmentF1(LaboratoryEquipmentF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _laboratoryEquipmentF1Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == 23 || c.CategoryId == 24 || c.CategoryId == 25), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/CreateLaboratoryEquipmentF1.cshtml", product);
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        var filePath = Path.Combine(directoryPath, fileName);
        if (System.IO.File.Exists(filePath))
        {
          fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, fileName);
        }
        product.image = fileName;
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }

      await _laboratoryEquipmentF1Service.AddProductAsync(product);
      return RedirectToAction(product.CategoryId switch
      {
        23 => "LaboratoryEquipmentF1",
        24 => "LabEquipF1Ketquahieuchuan",
        25 => "LabEquipF1DailyCheck",
        _ => "LaboratoryEquipmentF1"
      });
    }

    // Edit
    public async Task<IActionResult> EditLaboratoryEquipmentF1(int id)
    {
      var product = await _laboratoryEquipmentF1Service.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      var categories = await _laboratoryEquipmentF1Service.GetCategories();
      var filtered = categories.Where(c => c.CategoryId == 23 || c.CategoryId == 24 || c.CategoryId == 25);
      ViewBag.CategoryList = new SelectList(filtered, "CategoryId", "CategoryName", product.CategoryId);

      return View("~/Views/ProductQC/EditLaboratoryEquipmentF1.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLaboratoryEquipmentF1(LaboratoryEquipmentF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _laboratoryEquipmentF1Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == 23 || c.CategoryId == 24 || c.CategoryId == 25), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/EditLaboratoryEquipmentF1.cshtml", product);
      }

      var existing = await _laboratoryEquipmentF1Service.GetProductByIdAsync(product.ProductId);
      if (existing == null) return NotFound();

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        var filePath = Path.Combine(directoryPath, fileName);
        if (System.IO.File.Exists(filePath))
        {
          fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, fileName);
        }
        product.image = fileName;
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }
      else
      {
        product.image = existing.image;
      }

      await _laboratoryEquipmentF1Service.UpdateProductAsync(product);
      return RedirectToAction(product.CategoryId switch
      {
        23 => "LaboratoryEquipmentF1",
        24 => "LabEquipF1Ketquahieuchuan",
        25 => "LabEquipF1DailyCheck",
        _ => "LaboratoryEquipmentF1"
      });
    }

    // Xóa
    [HttpPost]
    public async Task<IActionResult> DeleteLaboratoryEquipmentF1(int productId)
    {
      if (productId <= 0)
      {
        return Json(new { success = false, message = "ID không hợp lệ." });
      }
      await _laboratoryEquipmentF1Service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Xóa thành công!" });
    }

    // Chi tiết
    public async Task<IActionResult> ShowLaboratoryEquipmentF1(int id)
    {
      var product = await _laboratoryEquipmentF1Service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/ProductQC/ShowLaboratoryEquipmentF1.cshtml", product);
    }
  }
}
