using ProductsTask.Dto.Product;
using ProductsTask.IData.Interfaces;
using ProductsTask.SharedKernal.Enums;
using ProductsTask.SharedKernal.Extension;
using ProductsTask.SharedKernal.Messages;
using ProductsTask.SharedKernal.OperationResults;
using ProductsTask.SqlServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsTask.Data.Repositories
{
    public class ProductRepository : BasicRepository, IProductRepository
    {
        #region Constructor
        public ProductRepository(PTDbContext context) : base(context) { }
        #endregion

        #region Get Products
        public OperationResult<GenericOperationResult, IEnumerable<ProductDto>> GetProducts(
            string query, ProductStatus? productStatus, bool enablePagination, int pageSize, int pageNumber)
        {
            var result = new OperationResult<GenericOperationResult,
                IEnumerable<ProductDto>>(GenericOperationResult.Success);

            try
            {
                var data = Context.Products
                    .Where(productEntity => productEntity.IsValid
                    && (
                    string.IsNullOrEmpty(query) || productEntity.Name.Contains(query)
                    || productEntity.Description.Contains(query))
                    && (productStatus == null || productEntity.ProductStatus == productStatus.Value));

                if (enablePagination)
                {
                    data = ExtensionMethods.GetDataWithPagination(data, pageSize, pageNumber);
                }

                var resultData = data.Select(productEntity => new ProductDto
                {
                    Id = productEntity.Id,
                    Name = productEntity.Name,
                    Description = productEntity.Description,
                    BarCode = productEntity.BarCode,
                    Weight = productEntity.Weight,
                    ProductStatus = productEntity.ProductStatus
                }).ToList();

                return result.UpdateResultData(resultData);
            }
            catch (Exception)
            {
                return result.AddError(ErrorMessages.InternalServerError)
                    .UpdateResultStatus(GenericOperationResult.Failed);
            }
        }
        #endregion

        #region Get Products Count
        public OperationResult<GenericOperationResult, ProductsCountDto> GetProductsCount()
        {
            var result = new OperationResult<GenericOperationResult,
                ProductsCountDto>(GenericOperationResult.Success);

            try
            {
                var data = new ProductsCountDto
                {
                    SoldProducts = Context.Products
                   .Where(prEntity => prEntity.IsValid && prEntity.ProductStatus == ProductStatus.Sold)
                   .Count(),

                    DamagedProducts = Context.Products
                   .Where(prEntity => prEntity.IsValid && prEntity.ProductStatus == ProductStatus.Damaged)
                   .Count(),

                    StockProducts = Context.Products
                   .Where(prEntity => prEntity.IsValid && prEntity.ProductStatus == ProductStatus.InStock)
                   .Count()
                };

                return result.UpdateResultData(data);
            }
            catch (Exception)
            {
                return result.AddError(ErrorMessages.InternalServerError)
                    .UpdateResultStatus(GenericOperationResult.Failed);
            }
        }
        #endregion

        #region Change Status Of Product
        public OperationResult<GenericOperationResult> ChangeStatusOfProduct(
            int productId, ProductStatus productStatus)
        {
            var result = new OperationResult<GenericOperationResult>(GenericOperationResult.Success);

            try
            {
                var entity = Context.Products
               .Where(prodEntity => prodEntity.IsValid && prodEntity.Id == productId)
               .SingleOrDefault();

                if (entity is null)
                {
                    return result.AddError(ErrorMessages.ProductNotFound)
                        .UpdateResultStatus(GenericOperationResult.NotFound);
                }

                entity.ProductStatus = productStatus;
                Context.Update(entity);
                Context.SaveChanges();

                return result;
            }
            catch (Exception)
            {
                return result.AddError(ErrorMessages.InternalServerError)
                    .UpdateResultStatus(GenericOperationResult.Failed);
            }
        }
        #endregion

        #region Sell Prdoduct
        public OperationResult<GenericOperationResult> SellProduct(int productId)
        {
            var result = new OperationResult<GenericOperationResult>(GenericOperationResult.ValidationError);

            try
            {
                var entity = Context.Products
               .Where(prodEntity => prodEntity.IsValid && prodEntity.Id == productId)
               .SingleOrDefault();

                if (entity is null)
                {
                    return result.AddError(ErrorMessages.ProductNotFound)
                        .UpdateResultStatus(GenericOperationResult.NotFound);
                }

                if (entity.ProductStatus == ProductStatus.InStock)
                {
                    entity.ProductStatus = ProductStatus.Sold;
                    Context.Update(entity);
                    Context.SaveChanges();

                    return result.UpdateResultStatus(GenericOperationResult.Success);
                }
                var errorMessage = GetErrorMessageDependingOnProductStatus(entity.ProductStatus);

                return result.AddError(errorMessage);
            }
            catch (Exception)
            {
                return result.AddError(ErrorMessages.InternalServerError)
                    .UpdateResultStatus(GenericOperationResult.Failed);
            }
        }
        private string GetErrorMessageDependingOnProductStatus(ProductStatus productStatus)
        {
            return productStatus switch
            {
                ProductStatus.Damaged => ErrorMessages.ProductWasDamaged,
                ProductStatus.Sold => ErrorMessages.ProductWasSold,
                _ => string.Empty
            };
        }
        #endregion
    }
}
