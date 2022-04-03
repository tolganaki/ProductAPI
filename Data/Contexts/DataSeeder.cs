using ProductAPI.Data.Entities;
using ProductAPI.Domain.Enum;

namespace ProductAPI.Data.Contexts
{
    public static class DataSeeder
    {
        public static async Task Seed(AplicationDbContext context)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Product1",
                    TypeId = (byte)ProductType.ProductType1,
                    Category = "Category1"
                },
                new Product
                {
                    Id = 2,
                    Name = "Product2",
                    TypeId = (byte)ProductType.ProductType2,
                    Category = "Category1"
                }
                ,
                new Product
                {
                    Id = 3,
                    Name = "Product3",
                    TypeId = (byte)ProductType.ProductType1,
                    Category = "Category2"
                }
            };
            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}