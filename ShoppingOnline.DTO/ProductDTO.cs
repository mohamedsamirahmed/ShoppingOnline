using System;
using System.Collections.Generic;

namespace ShoppingOnline.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public List<ProductCategoryDTO> ProductCategories { get; set; }

        public ICollection<PhotoDTO> Photos { get; set; }
    }
}
