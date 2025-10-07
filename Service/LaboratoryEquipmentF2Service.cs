using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class LaboratoryEquipmentF2Service : ILaboratoryEquipmentF2Service
  {
    private readonly ILaboratoryEquipmentF2Repository _laboratoryEquipmentF2Repository;

    public LaboratoryEquipmentF2Service(ILaboratoryEquipmentF2Repository laboratoryEquipmentF2Repository)
    {
      _laboratoryEquipmentF2Repository = laboratoryEquipmentF2Repository;
    }

    public async Task AddProductAsync(LaboratoryEquipmentF2DTO product)
    {
      await _laboratoryEquipmentF2Repository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _laboratoryEquipmentF2Repository.GetCategories();
    }

    public async Task<IPagedList<LaboratoryEquipmentF2DTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _laboratoryEquipmentF2Repository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _laboratoryEquipmentF2Repository.DeleteProductAsync(productId);
    }

    public async Task<LaboratoryEquipmentF2DTO> GetProductByIdAsync(int productId)
    {
      return await _laboratoryEquipmentF2Repository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(LaboratoryEquipmentF2DTO product)
    {
      await _laboratoryEquipmentF2Repository.UpdateProductAsync(product);
    }

    public async Task<IEnumerable<LaboratoryEquipmentF2DTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _laboratoryEquipmentF2Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<LaboratoryEquipmentF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _laboratoryEquipmentF2Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }
  }
}
