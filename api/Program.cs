namespace api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

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
