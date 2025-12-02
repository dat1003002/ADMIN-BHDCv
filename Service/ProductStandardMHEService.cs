using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class ProductStandardMHEService : IProductStandardMHEService
  {
    private readonly IProductStandardMHERepository _repo;

    public ProductStandardMHEService(IProductStandardMHERepository repo)
    {
      _repo = repo;
    }

    public async Task AddProductAsync(ProductStandardMHEDTO p) => await _repo.AddProductAsync(p);
    public async Task<IEnumerable<Category>> GetCategories() => await _repo.GetCategories();
    public async Task<IPagedList<ProductStandardMHEDTO>> GetProducts(int cId, int p, int ps)
        => await (await _repo.GetProducts(cId)).ToPagedListAsync(p, ps);
    public async Task DeleteProductAsync(int id) => await _repo.DeleteProductAsync(id);
    public async Task<ProductStandardMHEDTO> GetProductByIdAsync(int id) => await _repo.GetProductByIdAsync(id);
    public async Task UpdateProductAsync(ProductStandardMHEDTO p) => await _repo.UpdateProductAsync(p);
    public async Task<IPagedList<ProductStandardMHEDTO>> SearchProductsByNameAsync(string n, int cId, int p, int ps)
        => await (await _repo.SearchProductsByNameAsync(n, cId)).ToPagedListAsync(p, ps);
  }
}
