using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }

        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Quantitiy { get; set; }

        public double Price { get; set; }
    }
}
