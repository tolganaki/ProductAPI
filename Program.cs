using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Data.Contexts;
using ProductAPI.Data.Repositories;
using ProductAPI.Services.Products;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(q =>
{
    var xmlFile = "ProductAPI.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    q.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("in-memory"));
});

// configure dependency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// initialize dummy data
using var scope = app.Services.CreateScope();
try
{
    var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
    await DataSeeder.Seed(context);
}
catch (Exception ex)
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "There was an error during Data Seeding");
}

// Configure HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();