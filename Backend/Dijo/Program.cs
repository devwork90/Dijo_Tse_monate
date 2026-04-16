using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantAPI.API.Data;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Data.Data_seed;
using RestaurantAPI.Middlewares;
using RestaurantAPI.Repositories;
using RestaurantAPI.Service;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ---------------- LOGGING ----------------
var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/Dijo_log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// ---------------- SERVICES ----------------
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// FIXED SWAGGER CONFIG (FORCE HTTPS)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dijo API", Version = "v1" });

    // Force Swagger to always use HTTPS
    //options.AddServer(new OpenApiServer
    //{
    //    Url = "https://localhost:7065" // Make sure this matches your HTTPS port
    //});

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new List<string>()
        }
    });
});

// ---------------- DATABASE ----------------
builder.Services.AddDbContext<DijoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DijoConnectionString"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null
        )
    ));

// ---------------- DEPENDENCY INJECTION ----------------
builder.Services.AddScoped<IRestaurantRepository, SQLRestaurantRepository>();
builder.Services.AddScoped<IMenuRepository, SQLMenuRepository>();
builder.Services.AddScoped<ISubMenuRepository, SQLSubMenuRepository>();
builder.Services.AddScoped<IMenuItemRepository, SQLMenuItemRepository>();
builder.Services.AddScoped<IImageRepository, SQLImgeRepository>();

builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<ISubMenuService, SubMenuService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IImageService, ImageService>();

// ---------------- AUTH ----------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ---------------- CORS ---------------- c20000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// ---------------- DB SEEDING ----------------
using (var scope = app.Services.CreateScope())
{
    var logger1 = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<DijoDbContext>();

    var retries = 10;

    for (int i = 1; i <= retries; i++)
    {
        try
        {
            logger1.LogInformation($"DB init attempt {i}");

            dbContext.Database.Migrate();

            // Seed QA or Development data only in development environment
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                MenuIconSeeder.Seed(dbContext);
                RestaurantIconSeeder.Seed(dbContext);
            }

            logger1.LogInformation("Database ready");
            break;
        }
        catch (Exception ex)
        {
            logger1.LogWarning($"DB not ready: {ex.Message}");
            Thread.Sleep(5000);

            if (i == retries) throw;
        }
    }
}
// ---------------- PIPELINE ----------------

// Dev tools
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Security
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

// Custom middleware
app.UseMiddleware<ExceptionHandllerMiddleware>();
app.UseMiddleware<RestaurantContextMiddleware>();

// Static files
var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/Images"
});

// Endpoints
app.MapControllers();

app.Run();