using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IProductStandardService
  {
    Task AddProductAsync(ProductStandardDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<ProductStandardDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardDTO product);
    Task<IEnumerable<ProductStandardDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IPagedList<ProductStandardDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
