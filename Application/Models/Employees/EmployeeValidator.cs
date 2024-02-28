using FluentValidation;

namespace Application.Models.Employees;

public class EmployeeValidator : AbstractValidator<EmployeeModel>
{
    public EmployeeValidator()
    {
        RuleFor(model => model.FirstName).NotNull().MaximumLength(200);
        RuleFor(model => model.LastName).NotNull().MaximumLength(200);
        RuleFor(model => model.Email).NotNull().EmailAddress().MaximumLength(200);
        RuleFor(model => model.PhoneNumber).NotNull().MaximumLength(9).MinimumLength(9);
    }
}