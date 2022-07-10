using ProductsTask.Middleware;
using ProductsTask.SharedKernal.Enums;

namespace ProductsTask.Utils
{
    public static class Error
    {
        public static void ThrowExceptionError(
            this GenericOperationResult enumResult, string errorMessage)
        {
            switch (enumResult)
            {
                case GenericOperationResult.Failed:
                    throw new InternalServerException(errorMessage);
                case GenericOperationResult.NotFound:
                    throw new NotFoundException(errorMessage);
                case GenericOperationResult.ValidationError:
                    throw new BadRequestException(errorMessage);
                default:
                    throw new InternalServerException(errorMessage);
            }
        }
    }
}
