using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Employee;

public class EmployeeModel
{
    [NotNull] [MaxLength(200)] 
    public string FirstName { get; set; } = default!;
    [NotNull] [MaxLength(200)] 
    public string LastName { get; set; } = default!;
    [NotNull] [EmailAddress] [MaxLength(200)]
    public string Email { get; set; } = default!;
    [NotNull] [Phone] [MaxLength(9)]
    public string PhoneNumber { get; set; } = default!;
}