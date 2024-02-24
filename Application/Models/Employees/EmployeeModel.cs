namespace Application.Models.Employees;

public class EmployeeModel
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public string FullName => $"{FirstName} {LastName}";
}