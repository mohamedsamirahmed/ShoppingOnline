using System;
using System.Collections.Generic;

namespace ShoppingOnline.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        
        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }

        public int CategoryId { get; set; }

        public List<ProductCategoryDTO> ProductCategories { get; set; }

        public ICollection<PhotoDTO> Photos { get; set; }


        public string PhotoUrl { get; set; }
    }
}
