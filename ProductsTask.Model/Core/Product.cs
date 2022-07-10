using ProductsTask.Model.Basic;
using ProductsTask.SharedKernal.Enums;

namespace ProductsTask.Model.Core
{
    public class Product : BasicEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Weight { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
