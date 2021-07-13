using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OrderStatusId { get; set; }
        public LookupDTO OrderStatus { get; set; }

        public double TotalPrice { get; set; }
        public UserDTO User { get; set; }

        public string ShipmentAddress { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
