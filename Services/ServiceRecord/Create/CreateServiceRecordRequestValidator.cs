using FluentValidation;

namespace App.Services.Servicess.Create
{
    public class CreateServiceRecordRequestValidator : AbstractValidator<CreateServiceRecordRequest>
    {
        public CreateServiceRecordRequestValidator()
        {
            RuleFor(x => x.VehicleId)
                .GreaterThan(0).WithMessage("Araç ID geçerli olmalıdır.");

            RuleFor(x => x.ServiceDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Servis tarihi gelecekte olamaz.");

            RuleFor(x => x.ProcessedBy)
                .NotEmpty().WithMessage("İşlemi yapan kişi zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Servis ücreti negatif olamaz.");

            RuleFor(x => x.Explanation)
                .NotEmpty().WithMessage("Açıklama girilmelidir.")
                .MaximumLength(500);

            RuleFor(x => x.TechnicianName)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.TechnicianName));
        }
    }
}
