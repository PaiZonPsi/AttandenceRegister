using Application.Common;
using FluentValidation;

namespace Application.Models.Employees;

public class EmployeeValidator : AbstractValidator<EmployeeModel>
{
    public EmployeeValidator()
    {
        var maximumLength = 200;
        var maxLengthErrorMessage = $"{ErrorMessages.MaxLengthMessage} {maximumLength.ToString()} characters!";
        RuleFor(model => model.FirstName).NotNull().WithMessage(ErrorMessages.NotNullMessage).MaximumLength(maximumLength).WithMessage(maxLengthErrorMessage);
        RuleFor(model => model.LastName).NotNull().WithMessage(ErrorMessages.NotNullMessage).MaximumLength(maximumLength).WithMessage(maxLengthErrorMessage);
        RuleFor(model => model.Email).NotNull().WithMessage(ErrorMessages.NotNullMessage).EmailAddress().WithMessage(ErrorMessages.EmailFormatMessage).MaximumLength(maximumLength).WithMessage(maxLengthErrorMessage);
        RuleFor(model => model.PhoneNumber).NotNull().WithMessage(ErrorMessages.NotNullMessage).MaximumLength(9).WithMessage(ErrorMessages.PhoneNumberFormatMessage).MinimumLength(9).WithMessage(ErrorMessages.PhoneNumberFormatMessage);
    }
}