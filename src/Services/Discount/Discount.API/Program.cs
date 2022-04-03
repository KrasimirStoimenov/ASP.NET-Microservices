using Discount.Data.DataContext;
using Discount.Extensions;
using Discount.Repositories.Discount;
using Discount.Service.AutoMappingProfile;
using Discount.Service.Discounts;

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
builder.Services.AddScoped<IDiscountContext, DiscountContext>();
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddTransient<IDiscountService, DiscountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<Program>();

app.Run();
