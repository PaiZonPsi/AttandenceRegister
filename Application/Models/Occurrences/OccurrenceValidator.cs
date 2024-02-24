using FluentValidation;

namespace Application.Models.Occurrences;

public class OccurrenceValidator : AbstractValidator<OccurrenceModel>
{
    public OccurrenceValidator()
    {
        RuleFor(model => model.Title).NotNull().MaximumLength(200);
        RuleFor(model => model.Active).NotNull();
    }
}