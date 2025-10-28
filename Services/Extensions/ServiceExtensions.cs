using App.Repositories.Vehicles;
using App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Vehicles;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using AutoMapper;
using App.Services.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;



namespace App.Services.Extensions
{
    public static class ServiceExtensions
    {
       
            public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
            {
                 services.AddScoped<IVehicleService, VehicleService>();
                 services.AddFluentValidationAutoValidation();
                 services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                 services.AddAutoMapper(Assembly.GetExecutingAssembly());
                 services.AddExceptionHandler<CriticalExceptionsHandler>();
                 services.AddExceptionHandler<GlobalExceptionHandler>();
                 services.Configure<ApiBehaviorOptions>(Options => Options.SuppressModelStateInvalidFilter = true);


            return services;
            }
        
    }
}
