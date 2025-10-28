using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Servicess.Update
{
    public class UpdateServiceRecordRequestValidator : AbstractValidator<UpdateServiceRecordRequest>
    {
        public UpdateServiceRecordRequestValidator()
        {


            RuleFor(x => x.ServiceDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Servis tarihi gelecekte olamaz.");

            RuleFor(x => x.ProcessedBy)
                .NotEmpty().WithMessage("İşlemi yapan kişi zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Servis ücreti negatif olamaz.");

            RuleFor(x => x.Explanation)
                .NotEmpty().WithMessage("Açıklama girilmelidir.")
                .MaximumLength(500);

            RuleFor(x => x.TechnicianName)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.TechnicianName));


        }
    } }
