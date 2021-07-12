using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingOnline.API.Data
{
    public static class Seed
    {
        public static async Task SeedUser(UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<User>>(userData);
            if (users == null) return;

            var roles = new List<Role>
            {
                new Role(){ Name="Member" },
                new Role(){ Name="Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                if (user.UserName == "mohamed")
                {
                    await userManager.CreateAsync(user, "P@$$w0rd");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else {
                    await userManager.CreateAsync(user, "P@$$w0rd");
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }
        }

        public static async Task SeedCategory(ShoppingOnlineDBContext context)
        {
            if (await context.Categories.AnyAsync()) return;

            var categoryData = await System.IO.File.ReadAllTextAsync("Data/CategorySeedData.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
            if (categories == null) return;
            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedProduct(ShoppingOnlineDBContext context)
        {
            if (await context.Products.AnyAsync()) return;

            var productData = await System.IO.File.ReadAllTextAsync("Data/ProductSeedData.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            if (products == null) return;
            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedProductCategory(ShoppingOnlineDBContext context)
        {
            if (await context.productCategories.AnyAsync()) return;

            var productCategoryData = await System.IO.File.ReadAllTextAsync("Data/ProductCategorySeedData.json");
            var productCategories = JsonSerializer.Deserialize<List<ProductCategory>>(productCategoryData);
            if (productCategories == null) return;
            foreach (var peoductCategory in productCategories)
            {
                context.productCategories.Add(peoductCategory);
            }
            await context.SaveChangesAsync();
        }
    }
}
