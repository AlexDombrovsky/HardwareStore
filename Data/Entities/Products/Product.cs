using System.Collections.Generic;

namespace HardwareStore.Data.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}