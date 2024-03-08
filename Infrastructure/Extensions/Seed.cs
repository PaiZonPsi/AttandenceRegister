using Domain.Entities;

namespace Infrastructure.Extensions;

public class Seed
{
    private readonly AttendanceDbContext _context;

    public Seed(AttendanceDbContext context)
    {
        _context = context;
    }
    
    public void SeedEmployees()
    {
        if(_context.Employees.Any() == true)
            return;
        
        var employees = new List<Employee>()
        {
            new Employee("James", "Jameson", "jj@jj.com", "314456908"),
            new Employee("Adam", "Adams", "aa@aa.com", "132487762"),
            new Employee("Bob", "Baker", "bobby@baker.com", "198276122")
        };
        _context.Employees.AddRange(employees);
        _context.SaveChanges();
    }

    public void SeedOccurrences()
    {
        if(_context.Occurrences.Any() == true)
            return;
        var occurrences = new List<Occurrence>()
        {
            new Occurrence("L4", false),
            new Occurrence("Vacation", false),
            new Occurrence("Unjustified", false),
            new Occurrence("Attended", true)
        };
        _context.Occurrences.AddRange(occurrences);
        _context.SaveChanges();
    }
}