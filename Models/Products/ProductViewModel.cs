using System.Collections.Generic;
using HardwareStore.Data.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HardwareStore.Models.Products
{
    public class ProductViewModel : Product
    {
        public List<SelectListItem> AllCategories { get; set; }
        public List<IFormFile> UploadedPhotos { get; set; }
    }
}