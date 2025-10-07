using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class RawMaterialStandardService : IRawMaterialStandardService
  {
    private readonly IRawMaterialStandardRepository _rawMaterialStandardRepository;

    public RawMaterialStandardService(IRawMaterialStandardRepository rawMaterialStandardRepository)
    {
      _rawMaterialStandardRepository = rawMaterialStandardRepository;
    }

    public async Task AddProductAsync(RawMaterialStandardDTO product)
    {
      await _rawMaterialStandardRepository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _rawMaterialStandardRepository.GetCategories();
    }

    public async Task<IPagedList<RawMaterialStandardDTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _rawMaterialStandardRepository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _rawMaterialStandardRepository.DeleteProductAsync(productId);
    }

    public async Task<RawMaterialStandardDTO> GetProductByIdAsync(int productId)
    {
      return await _rawMaterialStandardRepository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(RawMaterialStandardDTO product)
    {
      await _rawMaterialStandardRepository.UpdateProductAsync(product);
    }

    public async Task<IEnumerable<RawMaterialStandardDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _rawMaterialStandardRepository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<RawMaterialStandardDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _rawMaterialStandardRepository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }
  }
}
