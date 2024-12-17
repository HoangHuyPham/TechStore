namespace api;

using api.Datas;
using api.Models;
using api.Repos;
using api.Repos.Interfaces;
using api.Services;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
        builder.Services.AddScoped<IRepository<ProductDetail>, ProductDetailRepository>();
        builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
        builder.Services.AddScoped<IRepository<Preview>, PreviewRepository>();
        builder.Services.AddScoped<IRepository<ProductOption>, ProductOptionRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();


        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi("/openapi/tech-store-api.json");
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/tech-store-api.json", "v1");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
