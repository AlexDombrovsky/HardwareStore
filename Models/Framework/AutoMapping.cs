using AutoMapper;
using HardwareStore.Data.Entities.Products;
using HardwareStore.Models.Products;

namespace HardwareStore.Models.Framework
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Order, OrderViewModel>();
        }
    }
}