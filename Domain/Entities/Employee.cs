using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class Employee : BaseEntity
{
    [NotNull] [MaxLength(200)] 
    public string FirstName { get; private set; }
    [NotNull] [MaxLength(200)] 
    public string LastName { get; private set; }
    [NotNull] [EmailAddress] [MaxLength(200)]
    public string Email { get; private set; }
    [NotNull] [Phone] [MaxLength(9)] [MinLength(9)]
    public string PhoneNumber { get; private set; }

    public Employee(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        
    }
    public void SetFirstName(string value) => FirstName = value;
    public void SetLastName(string value) => LastName = value;
    public void SetEmail(string value) => Email = value;
    public void SetPhoneNumber(string value) => PhoneNumber = value;
}