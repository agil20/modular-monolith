using Common.Exceptions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

using Modules.Baskets.Infrastructure.Extentions;
using Modules.Baskets.Infrastructure.Persistence;
using Modules.Categories.Extentions;
using Modules.Categories.Infrastructure.Persistence;
using Modules.Products.Application.Extentions;
using Modules.Products.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalException>();
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// =========================================================================
// YENİ: CORS QEYDİYYATI (Frontend-in API-yə qoşula bilməsi üçün mütləqdir)
// =========================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger qeydiyyatı
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("categories", new OpenApiInfo { Title = "Category Module API", Version = "v1" });
    c.SwaggerDoc("products", new OpenApiInfo { Title = "Products Module API", Version = "v1" });
    c.SwaggerDoc("baskets", new OpenApiInfo { Title = "Baskets Module API", Version = "v1" });
});

builder.Services.AddCategoriesModule();
builder.Services.AddProductsModule();
builder.Services.AddBasketModule();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddDbContext<CategoriesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<BasketDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();

        // Burada appsettings.json-dakı bulud məlumatları oxunur
        cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], h =>
        {
            h.Username(configuration["RabbitMQ:Username"]);
            h.Password(configuration["RabbitMQ:Password"]);
        });
    });
});
var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/categories/swagger.json", "Categories API");
        c.SwaggerEndpoint("/swagger/products/swagger.json", "Products API");
        c.SwaggerEndpoint("/swagger/baskets/swagger.json", "Baskets API");

        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
app.Run();