using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingOnline.DTO
{
   public class RegisterDTO
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
