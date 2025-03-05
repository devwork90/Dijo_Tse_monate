using Microsoft.EntityFrameworkCore;
using Dijo.API.Data;
using Dijo.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DijoDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DijoConnectionString")));

builder.Services.AddScoped<IRestaurantRepository, SQLRestaurantRepository>();
builder.Services.AddScoped<IMenuRepository, SQLMenuRepository>();
builder.Services.AddScoped<IMenuItemRepository, SQLMenuItemRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
