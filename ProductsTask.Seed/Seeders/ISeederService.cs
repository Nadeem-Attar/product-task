using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsTask.Seed.Seeders
{
    public interface ISeederService
    {
        Task CreateProducts();
    }
}
