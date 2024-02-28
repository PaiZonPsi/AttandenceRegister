using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class SeedingExtension
{
    public static ModelBuilder SeedEmployees(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee()
            {
                Id = 1,
                FirstName = "James",
                LastName = "Jameson",
                Email = "jj@jj.com",
                PhoneNumber = "314456908"
            },
            new Employee()
            {
                Id = 2,
                FirstName = "Adam",
                LastName = "Adams",
                Email = "aa@aa.com",
                PhoneNumber = "132487762"
            },
            new Employee()
            {
                Id = 3,
                FirstName = "Bob",
                LastName = "Baker",
                Email = "bobby@baker.com",
                PhoneNumber = "198276122"
            }
        );
        return modelBuilder;
    }

    public static ModelBuilder SeedOccurrences(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Occurrence>().HasData(
            new Occurrence()
            {
                Id = 1,
                Title = "L4",
                Active = false
            },
            new Occurrence()
            {
                Id = 2,
                Title = "Vacation",
                Active = false
            },
            new Occurrence()
            {
                Id = 3,
                Title = "Unjustified",
                Active = false
            }
        );
        return modelBuilder;
    }
}