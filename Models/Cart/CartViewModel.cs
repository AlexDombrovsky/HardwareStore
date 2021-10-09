using System.Collections.Generic;
using HardwareStore.Models.Products;

namespace HardwareStore.Models.Cart
{
    public class CartViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public int Total { get; set; }
    }
}