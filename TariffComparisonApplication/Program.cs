using Microsoft.EntityFrameworkCore;
using TariffComparisonApplication.HandlerService;
using TariffComparisonApplication.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ProductDataBaseContext>(opt => opt.UseInMemoryDatabase("ProductDataBaseContext"));
builder.Services.AddScoped<IProductCostComparisonHandler, ProductCostComparisonHandler>();
builder.Services.AddScoped<IProductDataHandler, ProductDataHandler>();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Verivox - Tariff Comparison API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product List");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
