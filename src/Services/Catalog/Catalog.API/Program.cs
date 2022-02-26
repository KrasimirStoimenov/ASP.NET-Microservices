using AutoMapper;
using Catalog.Data.Data;
using Catalog.Data.Data.Interfaces;
using Catalog.Repositories.Product;
using Catalog.Services.AutoMappingProfile;
using Catalog.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAntiforgery();
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<MappingProfile>();
});
builder.Services.AddTransient<ICatalogContext, CatalogContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
