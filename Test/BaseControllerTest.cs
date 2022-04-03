using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ProductAPI.Data;
using ProductAPI.Data.Contexts;
using ProductAPI.Data.Repositories;
using ProductAPI.Services.Products;

namespace ProductAPI.Test
{
    public abstract class BaseControllerTest
    {
        protected IConfiguration configuration;
        protected ServiceCollection services = null;
        protected ServiceProvider serviceProvider = null;
        private IServiceScope serviceScope = null;

        protected IProductService _productService;

        public BaseControllerTest()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.UnitTest.json", true)
                .AddEnvironmentVariables();

            configuration = configurationBuilder.Build();

            services = new ServiceCollection();

            services.AddControllers();
            services.AddLogging();
            services.AddEndpointsApiExplorer();
            services.AddDbContext<AplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(configuration.GetConnectionString("in-memory"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            serviceProvider = services.BuildServiceProvider();
            serviceScope = serviceProvider.CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<AplicationDbContext>();
            DataSeeder.Seed(context).Wait();
        }

        protected void Configure()
        {
            if (serviceScope != null)
                serviceScope.Dispose();

            serviceScope = serviceProvider.CreateScope();

            _productService = serviceScope.ServiceProvider.GetService<IProductService>();
        }
    }
}