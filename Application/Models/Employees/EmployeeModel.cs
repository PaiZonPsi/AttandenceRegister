using System.ComponentModel.DataAnnotations;

namespace Application.Models.Employees;

public class EmployeeModel
{
    public int Id { get; set; }
    [Required] [MaxLength(200)] 
    public string FirstName { get; set; } = string.Empty;
    [Required] [MaxLength(200)] 
    public string LastName { get; set; } = default!;
    [Required] [EmailAddress] [MaxLength(200)]
    public string Email { get; set; } = default!;
    [Required] [Phone] [StringLength(9)]
    public string PhoneNumber { get; set; } = default!;

    public string FullName => $"{FirstName} {LastName}";
}