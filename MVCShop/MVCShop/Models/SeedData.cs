using Microsoft.EntityFrameworkCore;
using MVCShop.Data;

namespace MVCShop.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCShopContext>>()))
            {
                // Look for any students.
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }

                context.Product.AddRange(
                    new Product
                    {
                        Name = "Product1",
                        Price = 7.94,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "Product2",
                        Price = 8.94,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Name = "Product3",
                        Price = 9.94,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Name = "Product4",
                        Price = 10.94,
                        CategoryId = 4
                    },
                    new Product
                    {
                        Name = "Product5",
                        Price = 11.94,
                        CategoryId = 5
                    }
                );

                context.Category.AddRange(
                    new Category
                    {
                        Name = "Category 1",
                        Description = "Loại sản phẩm 1"
                    },
                    new Category
                    {
                        Name = "Category 2",
                        Description = "Loại sản phẩm 2"
                    },
                    new Category
                    {
                        Name = "Category 3",
                        Description = "Loại sản phẩm 3"
                    },
                    new Category
                    {
                        Name = "Category 4",
                        Description = "Loại sản phẩm 4"
                    },
                    new Category
                    {
                        Name = "Category 5",
                        Description = "Loại sản phẩm 5"
                    }
                );
                context.Customer.AddRange(
                    new Customer
                    {
                        Name = "Khách hàng 1",
                        Address = "Địa chỉ 1",
                        Email = "kh1@example.com"
                    },
                    new Customer
                    {
                        Name = "Khách hàng 2",
                        Address = "Địa chỉ 2",
                        Email = "kh2@example.com"
                    },
                    new Customer
                    {
                        Name = "Khách hàng 3",
                        Address = "Địa chỉ 3",
                        Email = "kh3@example.com"
                    },
                    new Customer
                    {
                        Name = "Khách hàng 4",
                        Address = "Địa chỉ 4",
                        Email = "kh4@example.com"
                    },
                    new Customer
                    {
                        Name = "Khách hàng 5",
                        Address = "Địa chỉ 5",
                        Email = "kh5@example.com"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
