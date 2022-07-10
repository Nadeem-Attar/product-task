using ProductsTask.SharedKernal.Enums;

namespace ProductsTask.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Weight { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
