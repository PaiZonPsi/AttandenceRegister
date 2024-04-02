using Application.Common;
using FluentValidation;

namespace Application.Models.Attendances;

public class AttendanceValidator : AbstractValidator<AttendanceModel>
{
    public AttendanceValidator()
    {
        RuleFor(model => model.Description).MaximumLength(500);
        RuleFor(model => model.EmployeeId).NotNull();
        RuleFor(model => model.OccurrenceId).NotNull();
        RuleFor(model => model.OccurrenceStartDate).NotNull()
            .LessThanOrEqualTo(model => model.OccurrenceEndDate)
            .WithMessage(ErrorMessages.DatesInvalidMessage);
        RuleFor(model => model.OccurrenceEndDate).NotNull();
    }
}