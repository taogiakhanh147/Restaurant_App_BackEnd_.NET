using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp",
        builder =>
        {
            builder.WithOrigins("https://restaurant-app-liart-gamma.vercel.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS middleware with named policy
app.UseCors("AllowFrontendApp");

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Routing and Authorization
app.UseAuthorization();
app.MapControllers();

app.Run();
