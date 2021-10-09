using System.Threading.Tasks;
using HardwareStore.Data.Entities.Products;
using Microsoft.AspNetCore.Http;

namespace HardwareStore.Interfaces
{
    public interface IPhotoService
    {
        Task<Photo> Create(IFormFile photo, int productId, string wwwroot);
        Task<bool> Delete(int id);
    }
}
