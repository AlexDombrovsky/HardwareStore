using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareStore.Data.Entities.Products;

namespace HardwareStore.Interfaces
{
    public interface IProductService
    {
        Task<Product> Create(Product entity);
        Task<Product> GetById(int id);
        Task<Product> Update(Product entity);
        Task<bool> Delete(int id);
        Task<List<Product>> GetAll();
        Task<List<Product>> GetLatestProducts(int lastProductsCount);
        List<Product> GetWithPagination(int page, int pageSize);
    }
}