using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// CORS cho Vercel frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVercelFrontend", builder =>
    {
        builder.WithOrigins("https://restaurant-app-liart-gamma.vercel.app")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS trước Swagger và Controllers
app.UseCors("AllowVercelFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
