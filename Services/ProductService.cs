﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareStore.Data;
using HardwareStore.Data.Entities.Products;
using HardwareStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Create(Product entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> GetById(int id)
        {
            var entity = await _dbContext.Products.Include(_ => _.Photos).FirstOrDefaultAsync(_ => _.Id == id);
            return entity;
        }

        public async Task<Product> Update(Product entity)
        {
            _dbContext.Products.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.Include(_ => _.Photos).ToListAsync();
        }

        public async Task<List<Product>> GetLatestProducts(int lastProductsCount)
        {
            return await _dbContext.Products.Include(_ => _.Photos).OrderBy(_ => _.Created).Take(lastProductsCount).ToListAsync();
        }

        public List<Product> GetWithPagination(int page, int pageSize)
        {
            return _dbContext.Products.Include(_ => _.Photos).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}