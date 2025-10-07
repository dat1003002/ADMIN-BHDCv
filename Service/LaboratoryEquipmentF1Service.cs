using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class LaboratoryEquipmentF1Service : ILaboratoryEquipmentF1Service
  {
    private readonly ILaboratoryEquipmentF1Repository _laboratoryEquipmentF1Repository;

    public LaboratoryEquipmentF1Service(ILaboratoryEquipmentF1Repository laboratoryEquipmentF1Repository)
    {
      _laboratoryEquipmentF1Repository = laboratoryEquipmentF1Repository;
    }

    public async Task AddProductAsync(LaboratoryEquipmentF1DTO product)
    {
      await _laboratoryEquipmentF1Repository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _laboratoryEquipmentF1Repository.GetCategories();
    }

    public async Task<IPagedList<LaboratoryEquipmentF1DTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _laboratoryEquipmentF1Repository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _laboratoryEquipmentF1Repository.DeleteProductAsync(productId);
    }

    public async Task<LaboratoryEquipmentF1DTO> GetProductByIdAsync(int productId)
    {
      return await _laboratoryEquipmentF1Repository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(LaboratoryEquipmentF1DTO product)
    {
      await _laboratoryEquipmentF1Repository.UpdateProductAsync(product);
    }

    public async Task<IEnumerable<LaboratoryEquipmentF1DTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _laboratoryEquipmentF1Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<LaboratoryEquipmentF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _laboratoryEquipmentF1Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }
  }
}
