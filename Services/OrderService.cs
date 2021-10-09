using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareStore.Data;
using HardwareStore.Data.Entities.Products;
using HardwareStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Create(Order entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Order>> GetOrdersByUserId(string userId)
        {
            var result = await _dbContext.Orders.Include(_ => _.Product.Photos).Where(_ => _.UserId == userId).ToListAsync();
            return result ?? new List<Order>();
        }

        public async Task<int> GetOrdersCountByUser(string userId)
        {
            return await _dbContext.Orders.Where(_ => _.UserId == userId).CountAsync();
        }

        public int GetTotalSum(string userId)
        {
            return _dbContext.Orders.Where(_ => _.UserId == userId).Select(_ => _.Product.Price).Sum();
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}