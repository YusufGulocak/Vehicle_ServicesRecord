using App.Repositories.Vehicles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Vehicles.Update
{
    public class UpdateVehicleRequestValidator : AbstractValidator<UpdateVehicleRequest>
    {
        
        public UpdateVehicleRequestValidator()
        {

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(v => v.Brand)
                .NotEmpty().WithMessage("Brand is required.")
                .MaximumLength(50).WithMessage("Brand cannot exceed 50 characters.");
            RuleFor(v => v.Year)
               .NotNull().WithMessage("Year is required.")
               .LessThanOrEqualTo(DateTime.Now).WithMessage("Year cannot be in the future.");
            RuleFor(v => v.NumberPlate)
                .NotEmpty().WithMessage("NumberPlate is required.")
                .MaximumLength(20).WithMessage("NumberPlate cannot exceed 20 characters.");
           
        }
    }
}