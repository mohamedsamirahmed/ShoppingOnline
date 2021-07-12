using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.DTO
{
    public class CartItemDTO
    {
        public int id { get; set; }
        
        public int productId { get; set; }

        public ProductDTO Product { get; set; }

        public int cartId { get; set; }
        public CartDTO Cart { get; set; }

        public int quantity { get; set; }

        public double price { get; set; }
    }
}
