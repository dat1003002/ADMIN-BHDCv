using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class ProductStandardService : IProductStandardService
  {
    private readonly IProductStandardRepository _productStandardRepository;

    public ProductStandardService(IProductStandardRepository productStandardRepository)
    {
      _productStandardRepository = productStandardRepository;
    }

    public async Task AddProductAsync(ProductStandardDTO product)
    {
      await _productStandardRepository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _productStandardRepository.GetCategories();
    }

    public async Task<IPagedList<ProductStandardDTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _productStandardRepository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _productStandardRepository.DeleteProductAsync(productId);
    }

    public async Task<ProductStandardDTO> GetProductByIdAsync(int productId)
    {
      return await _productStandardRepository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(ProductStandardDTO product)
    {
      await _productStandardRepository.UpdateProductAsync(product);
    }

    public async Task<IEnumerable<ProductStandardDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _productStandardRepository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<ProductStandardDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _productStandardRepository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }
  }
}
