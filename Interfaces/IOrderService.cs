using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareStore.Data.Entities.Products;

namespace HardwareStore.Interfaces
{
    public interface IOrderService
    {
        Task<Order> Create(Order entity);
        Task<List<Order>> GetOrdersByUserId(string userId);
        Task<int> GetOrdersCountByUser(string userId);
        int GetTotalSum(string userId);
        Task<bool> Delete(int id);
    }
}