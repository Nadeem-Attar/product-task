using ProductsTask.SharedKernal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsTask.SharedKernal.Extension
{
    public static class EnumsExtensionMethods
    {
        public const string ERROR = nameof(ERROR);
        public static string ProductStatusToString(this ProductStatus status)
        {
            return status switch
            {
                ProductStatus.Damaged => nameof(ProductStatus.Damaged),
                ProductStatus.InStock => nameof(ProductStatus.InStock),
                ProductStatus.Sold => nameof(ProductStatus.Sold),
                _ => ERROR
            };
        }
    }
}