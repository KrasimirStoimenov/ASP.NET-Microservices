using Discount.Data.DataContext;
using Discount.Extensions;
using Discount.Grpc.Services;
using Discount.Repositories.Discount;
using Discount.Service.AutoMappingProfile;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddAntiforgery();
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<MappingProfile>();
});
builder.Services.AddScoped<IDiscountContext, DiscountContext>();
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MigrateDatabase<Program>();

app.Run();
