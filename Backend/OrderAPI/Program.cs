using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Repositories;
using OrderAPI.Service;

var builder = WebApplication.CreateBuilder(args);

//-----------------------DATA-----------------------
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnectionString")));

// ---------------- DEPENDENCY INJECTION -----------
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddHttpClient<IRestaurantService, RestaurantService>(client => 
{
    client.BaseAddress = new Uri("https://localhost:7065/");
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine(app.Environment.EnvironmentName);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
