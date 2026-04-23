using App.Repositories.Extensions;
using App.Services.Extensions;
using System.Net;
using App.Services;
using App.Services.Mapping;
using App.Repositories.Services;
using Scalar.AspNetCore;
using App.Repositories.User;
using App.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!))
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();

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

// Repositories & Services
builder.Services.AddRepositories(builder.Configuration)
                .AddServices(builder.Configuration);

builder.Services.AddScoped<IServiceRecordRepository, ServiceRecordRepository>();
builder.Services.AddScoped<App.Services.Services.IServiceRecordService, App.Services.Services.ServiceRecordService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
    app.MapScalarApiReference();
}

app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                StatusCode = 500,
                error.Error.Message
            }));
        }
    });
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
