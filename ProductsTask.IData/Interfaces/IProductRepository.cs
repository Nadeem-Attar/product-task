using ProductsTask.Dto.Product;
using ProductsTask.SharedKernal.Enums;
using ProductsTask.SharedKernal.OperationResults;
using System.Collections.Generic;

namespace ProductsTask.IData.Interfaces
{
    public interface IProductRepository
    {
        OperationResult<GenericOperationResult, IEnumerable<ProductDto>> GetProducts(
            string query, ProductStatus? productStatus, bool enablePagination, int pageSize, int pageNumber);
        OperationResult<GenericOperationResult, ProductsCountDto> GetProductsCount();
        OperationResult<GenericOperationResult> ChangeStatusOfProduct(
            int productId, ProductStatus productStatus);
        OperationResult<GenericOperationResult> SellProduct(int productId);
    }
}
