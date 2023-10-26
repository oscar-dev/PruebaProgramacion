using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProyectoPrueba.Models;
using ProyectoPrueba.Interfaces;
using ProyectoPrueba.Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ProyectoPrueba;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<BaseContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbmaster")));

builder.Services.AddDbContext<ProductContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbproduct")));

builder.Services.AddDbContext<Product2Context>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbproduct2")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new AutoFacModule()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

using ( var scope = app.Services.CreateScope() )
{
    var context = scope.ServiceProvider.GetRequiredService<BaseContext>();
    context.Database.Migrate();
}

//app.MapControllers();

app.MapControllerRoute(name: "products",
               pattern: "{slug}/products/{id?}",
               defaults: new { controller="Product", action="Index" } );

app.MapControllerRoute(name: "default",
               pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
