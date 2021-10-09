using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using HardwareStore.Data;
using HardwareStore.Interfaces;
using HardwareStore.Models.Framework;
using HardwareStore.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IMapper mapper, IPhotoService photoService, IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _photoService = photoService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
        {
            var productsWithPagination = _mapper.Map<List<ProductViewModel>>(_productService.GetWithPagination(page, pageSize));
            var allProducts = await _productService.GetAll();
            return View(GetPaginationModel(productsWithPagination, allProducts.Count, page, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(model);

                if (model.UploadedPhotos != null)
                    foreach (var photo in model.UploadedPhotos)
                        model.Photos.Add(await _photoService.Create(photo, model.Id,
                            _webHostEnvironment.WebRootPath));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetById(id);
            var model = _mapper.Map<ProductViewModel>(product);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(model);

                if (model.UploadedPhotos != null)
                    foreach (var photo in model.UploadedPhotos)
                        model.Photos.Add(await _photoService.Create(photo, model.Id,
                            _webHostEnvironment.WebRootPath));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetById(id);
            var model = _mapper.Map<ProductViewModel>(product);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id, int page)
        {
            var product = await _productService.GetById(id);
            var result = await _productService.Delete(id);
            if (result)
            {
                foreach (var photo in product.Photos) await _photoService.Delete(photo.Id);

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, WorkContext.ImagePath, product.Id.ToString());
                if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true);
            }

            return RedirectToAction("Index", new {page});
        }

        private ProductPaginationViewModel GetPaginationModel(List<ProductViewModel> productModels, int count, int page, int pageSize)
        {
            var products = _mapper.Map<List<ProductViewModel>>(productModels);
            var pager = new Pager(count, page, pageSize);
            var model = new ProductPaginationViewModel
            {
                Items = products,
                Pager = pager
            };
            return model;
        }
    }
}