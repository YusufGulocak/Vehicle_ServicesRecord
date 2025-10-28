using App.Repositories;
using App.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using App.Services.Extensions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using App.Services;
using App.Services.Mapping;
using App.Repositories.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(Options =>
{
    Options.Filters.Add<FluentValidationFilter>();
    Options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => options.Filters.Add<FluentValidationFilter>());
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Swagger
builder.Services.AddSwaggerGen(options =>
{

    options.SupportNonNullableReferenceTypes();
    options.UseAllOfToExtendReferenceSchemas();
    options.MapType<HttpStatusCode>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "integer",
        Format = "int32",
        Description = "HTTP status code"
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories & Services
builder.Services.AddRepositories(builder.Configuration)
                .AddServices(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceRecordRepository, ServiceRecordRepository>();
builder.Services.AddScoped<App.Services.Services.IServiceRecordService, App.Services.Services.ServiceRecordService>();
// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();     
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");  // <<< ÖNEMLÝ: Authorization’dan önce olmalý

app.UseAuthorization();

app.MapControllers();

// Global exception handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal Server Error");
    }
});

app.Run();
app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                StatusCode = 500,
                Message = ex.Message
            }));
        }
    });
});
