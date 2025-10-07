using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IRawMaterialStandardService
  {
    Task AddProductAsync(RawMaterialStandardDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<RawMaterialStandardDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialStandardDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialStandardDTO product);
    Task<IEnumerable<RawMaterialStandardDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IPagedList<RawMaterialStandardDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
