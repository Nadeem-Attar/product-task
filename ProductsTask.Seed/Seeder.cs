using ProductsTask.Seed.Seeders;
using System;
using System.Threading.Tasks;

namespace ProductsTask.Seed
{
    public static class Seeder
    {
        public async static Task SeedProductsAsync(IServiceProvider serviceProvider)
        {
            var seederService = new SeederService(serviceProvider);

            await seederService.CreateProducts();
        }
    }
}
