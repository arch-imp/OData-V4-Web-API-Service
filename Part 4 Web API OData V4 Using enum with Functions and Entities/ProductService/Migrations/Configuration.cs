using System.Data.Entity.Migrations;
using ProductService.Models;

namespace ProductService.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProductsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProductsContext context)
        {
            context.Suppliers.AddOrUpdate(
                new Supplier { Id = 1, Name = "Contoso, Ltd." }, 
                new Supplier { Id = 2, Name = "Fabrikam, Inc." }, 
                new Supplier { Id = 3, Name = "Wingtip Toys" }
            );

            context.Products.AddOrUpdate(
                new Product { Id = 1, Name = "Hat", Price = 15, Category = "Apparel", SupplierId = 1 }, 
                new Product { Id = 2, Name = "Socks", Price = 5, Category = "Apparel", SupplierId = 2 }, 
                new Product { Id = 3, Name = "Scarf", Price = 12, Category = "Apparel", SupplierId = 1 }, 
                new Product { Id = 4, Name = "Yo-yo", Price = 4.95M, Category = "Toys", SupplierId = 3 }, 
                new Product { Id = 5, Name = "Puzzle", Price = 8, Category = "Toys", SupplierId = 3 }
            );
        }
    }
}
