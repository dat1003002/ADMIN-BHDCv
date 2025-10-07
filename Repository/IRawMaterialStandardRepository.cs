using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IRawMaterialStandardRepository
  {
    Task AddProductAsync(RawMaterialStandardDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<RawMaterialStandardDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialStandardDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialStandardDTO productDTO);
    Task<IQueryable<RawMaterialStandardDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IEnumerable<RawMaterialStandardDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
