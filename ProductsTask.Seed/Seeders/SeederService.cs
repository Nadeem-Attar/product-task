using Microsoft.Extensions.DependencyInjection;
using ProductsTask.Model.Core;
using ProductsTask.SharedKernal.Enums;
using ProductsTask.SqlServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsTask.Seed.Seeders
{
    public class SeederService : ISeederService
    {
        private PTDbContext _context { get; }
        public SeederService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<PTDbContext>();
        }
        public async Task CreateProducts()
        {
            var productsEntity = _context.Products.FirstOrDefault(p => p.IsValid);
            if (productsEntity is null)
            {
                List<Product> products = new List<Product>();

                var soldProducts = CreateProductsAccordingToProductStatus(ProductStatus.Sold);
                var DamagedProducts = CreateProductsAccordingToProductStatus(ProductStatus.Damaged);
                var StockProducts = CreateProductsAccordingToProductStatus(ProductStatus.InStock);

                products.AddRange(soldProducts);
                products.AddRange(DamagedProducts);
                products.AddRange(StockProducts);
                _context.AddRange(products);

                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Product> CreateProductsAccordingToProductStatus(
            ProductStatus productStatus)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < 3; i++)
            {
                var product = new Product
                {
                    Name = $"Prodcut Name {i + 1}",
                    Description = $"Prodcut Description {i + 1}",
                    Weight = 12.45,
                    ProductStatus = productStatus
                };
                products.Add(product);
            }
            return products;
        }
    }
}
