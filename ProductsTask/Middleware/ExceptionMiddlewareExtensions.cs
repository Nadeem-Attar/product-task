using Microsoft.AspNetCore.Builder;

namespace ProductsTask.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
