using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IProductStandardRepository
  {
    Task AddProductAsync(ProductStandardDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<ProductStandardDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardDTO productDTO);
    Task<IQueryable<ProductStandardDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IEnumerable<ProductStandardDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
