using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingOnline.Domain.Model
{
    [Table("User")]
    public class User:IdentityUser<int>
    {

        public ICollection<UserRole> UserRoles { get; set; }


        public ICollection<Product> Products { get; set; }

        //[Key]
        //public int Id { get; set; }
        //public string Name { get; set; }

        //public bool IsAdmin { get; set; }

        //public byte[] PasswordHash { get; set; }

        //public byte[] PasswordSalt { get; set; }
    }
}
