using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.DTO
{
    public class CartDTO
    {
        public int id { get; set; }

        public int CartStatusId { get; set; }

        public UserDTO User { get; set; }
    }
}
