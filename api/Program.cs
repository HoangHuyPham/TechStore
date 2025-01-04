namespace api;

using System.Text;
using api.Datas;
using api.Models;
using api.Repos;
using api.Repos.Interfaces;
using api.Services;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "Name",
                    RoleClaimType = "Role",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:SecretToken").Value!)),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            }
        );
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TechStore API",
                Description = "Manage TechStore items",
                TermsOfService = new Uri("https://example.com/terms"),
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid JWT Bearer token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
        builder.Services.AddScoped<IRepository<ProductDetail>, ProductDetailRepository>();
        builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
        builder.Services.AddScoped<IRepository<Preview>, PreviewRepository>();
        builder.Services.AddScoped<IRepository<ProductOption>, ProductOptionRepository>();
        builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IAuthService, AuthService>();


        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
