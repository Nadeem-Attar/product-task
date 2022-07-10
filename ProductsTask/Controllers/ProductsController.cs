using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductsTask.IData.Interfaces;
using ProductsTask.Middleware;
using ProductsTask.SharedKernal.Enums;
using ProductsTask.SharedKernal.Messages;
using ProductsTask.Utils;

namespace ProductsTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Properties & Constructor
        private IProductRepository _productRepository { get; }
        private IStringLocalizer<ProductsController> FromResx { get; }
        public ProductsController(
            IProductRepository productRepository,
            IStringLocalizer<ProductsController> fromResx)
        {
            _productRepository = productRepository;
            FromResx = fromResx;
        }
        #endregion

        #region Get Products
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProducts(
            string query, ProductStatus? productStatus,
            bool enablePagination, int pageSize = 10, int pageNumber = 0)
        {
            var result = _productRepository.GetProducts(
                query, productStatus, enablePagination, pageSize, pageNumber);
            if (result.EnumResult != GenericOperationResult.Success)
                result.EnumResult.ThrowExceptionError(FromResx[result.ErrorMessages]);

            return Ok(result);
        }
        #endregion

        #region Get Products Count
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProductsCount()
        {
            var result = _productRepository.GetProductsCount();
            if (result.EnumResult != GenericOperationResult.Success)
                result.EnumResult.ThrowExceptionError(FromResx[result.ErrorMessages]);

            return Ok(result);
        }
        #endregion

        #region Change Status Of Product
        [HttpPut]
        [Route("[action]")]
        public IActionResult ChangeStatusOfProduct(int productId, ProductStatus? productStatus)
        {
            if (productStatus is null)
                throw new BadRequestException(FromResx[ErrorMessages.ChooseProductStatus]);

            var result = _productRepository.ChangeStatusOfProduct(productId, productStatus.Value);

            if (result.EnumResult != GenericOperationResult.Success)
                result.EnumResult.ThrowExceptionError(FromResx[result.ErrorMessages]);

            return Ok(result);
        }
        #endregion

        #region Sell Product
        [HttpPut]
        [Route("[action]")]
        public IActionResult SellProduct(int productId)
        {
            var result = _productRepository.SellProduct(productId);

            if (result.EnumResult != GenericOperationResult.Success)
                result.EnumResult.ThrowExceptionError(FromResx[result.ErrorMessages]);

            return Ok(result);
        }
        #endregion
    }
}
