using Application.Common;
using FluentValidation;

namespace Application.Models.Occurrences;

public class OccurrenceValidator : AbstractValidator<OccurrenceModel>
{
    public OccurrenceValidator()
    {
        var maximumLength = 200;
        var maxLengthErrorMessage = $"{ErrorMessages.MaxLengthMessage} {maximumLength} characters!";
        RuleFor(model => model.Title).NotNull()
            .MaximumLength(maximumLength)
            .WithMessage(maxLengthErrorMessage);
        RuleFor(model => model.Active).NotNull();
    }
}