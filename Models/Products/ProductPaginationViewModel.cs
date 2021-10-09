using System.Collections.Generic;
using HardwareStore.Models.Framework;

namespace HardwareStore.Models.Products
{
    public class ProductPaginationViewModel
    {
        public List<ProductViewModel> Items { get; set; } = new List<ProductViewModel>();
        public Pager Pager { get; set; }
    }
}