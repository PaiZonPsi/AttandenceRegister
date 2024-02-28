using FluentValidation;

namespace Application.Models.Attendances;

public class AttendanceValidator : AbstractValidator<AttendanceModel>
{
    public AttendanceValidator()
    {
        RuleFor(model => model.Description).MaximumLength(500);
        RuleFor(model => model.OccurrenceStartDate).NotNull();
        RuleFor(model => model.OccurrenceEndDate).NotNull();
        RuleFor(model => model.EmployeeId).NotNull();
        RuleFor(model => model.OccurrenceId).NotNull();
    }
}