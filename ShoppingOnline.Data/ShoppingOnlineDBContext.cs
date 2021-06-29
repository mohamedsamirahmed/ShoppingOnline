using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Domain.Model;

namespace ShoppingOnline.Data
{
    public class ShoppingOnlineDBContext : DbContext
    {
        public ShoppingOnlineDBContext(DbContextOptions<ShoppingOnlineDBContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> productCategories { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.CategoryId, pc.ProductId });

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.Entity<CartStatus>(entity => { entity.HasIndex(e => e.Id).IsUnique(); });

            
            builder.Entity<CartStatus>().HasData(
            new CartStatus {Id=1, Name = "New" },
            new CartStatus {Id=2, Name = "Checkedout" },
            new CartStatus {Id=3, Name = "Cancelled" }
            );

            builder.Entity<OrderStatus>().HasData(
                new OrderStatus {Id=1, Name = "New" },
                new OrderStatus { Id = 2, Name = "Checkedout" },
                new OrderStatus { Id = 3, Name = "Paid" },
                new OrderStatus { Id = 4, Name = "Shipped" },
                new OrderStatus { Id = 5, Name = "Delivered" },
                new OrderStatus { Id = 6, Name = "Cancelled" }
                );

            builder.Entity<Category>().HasData(
                new Category {Id=1, Name = "Mobiles" },
                new Category {Id=2, Name = "Computers" }
                );

            builder.Entity<User>().HasData(
                    new User { Id = 1, Name = "Samir", IsAdmin = true },
                    new User { Id=2,Name = "Wael", IsAdmin = false }
                );

            builder.Entity<Product>().HasData(
                    new Product {Id=1, Name = "iphone12", Description = "iphone12 black 128GB", UserId = 1 },
                    new Product { Id = 2, Name = "iphone6", Description = "iphone6 black 8GB", UserId = 1 },
                    new Product { Id = 3, Name = "Samsung Note", Description = "Samsung Note 8 64GB", UserId = 1 },
                    new Product { Id = 4, Name = "Pixel 4", Description = "Google Pixel 4a 64 GB", UserId = 1 },
                    new Product { Id = 5, Name = "Pixel 5", Description = "Google Pixel 5 128 GB", UserId = 1 }
                );

            builder.Entity<ProductCategory>().HasData(
                    new ProductCategory { ProductId = 1, CategoryId = 1 },
                    new ProductCategory { ProductId = 2, CategoryId = 1 },
                    new ProductCategory { ProductId = 3, CategoryId = 1 },
                    new ProductCategory { ProductId = 4, CategoryId = 1 },
                    new ProductCategory { ProductId = 5, CategoryId = 1 }
                );


        }


    }
    }
