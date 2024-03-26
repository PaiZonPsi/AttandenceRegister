namespace Domain.Entities;

public class EmployeeSummary
{
    public int Year { get; init; }
    public string EmployeeFullName { get; init; } = string.Empty;
    public string OccurrenceTitle { get; init; } = string.Empty;
    public int AbsentDays { get; init; } 
}