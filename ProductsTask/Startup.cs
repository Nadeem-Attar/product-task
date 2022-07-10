using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductsTask.SqlServer.Database;
using Microsoft.EntityFrameworkCore;
using ProductsTask.IData.Interfaces;
using ProductsTask.Data.Repositories;
using ProductsTask.Middleware;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace ProductsTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ProductsTask",
                        Version = "v1"
                    });
            });

            services
                .AddDbContext<PTDbContext>(options =>
                {
                    var connectionString = Configuration.GetConnectionString("Default");
                    options.UseSqlServer(connectionString);
                });

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCuture = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar"),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCuture;
                options.SupportedUICultures = supportedCuture;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsTask v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            var locOptions = app.ApplicationServices.GetService
                <IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(locOptions.Value);

            app.ConfigureCustomExceptionMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
