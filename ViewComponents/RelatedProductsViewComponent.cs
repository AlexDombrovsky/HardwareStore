using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HardwareStore.Interfaces;
using HardwareStore.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.ViewComponents
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private const int LatestProductsCount = 16;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public RelatedProductsViewComponent(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await GetLatestProducts();
            return View(model);
        }

        public async Task<List<ProductViewModel>> GetLatestProducts()
        {
            var products = await _productService.GetLatestProducts(LatestProductsCount);
            var model = _mapper.Map<List<ProductViewModel>>(products);
            return model;
        }
    }
}