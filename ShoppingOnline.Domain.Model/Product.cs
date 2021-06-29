﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingOnline.Domain.Model
{
    [Table("Product")]
    public class Product {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public double Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

    }
}
