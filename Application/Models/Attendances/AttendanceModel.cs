using Domain.Entities;

namespace Application.Models.Attendances;

public class AttendanceModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int OccurrenceId { get; set; }
    public Occurrence? Occurrence { get; set; }
    public string? Description { get; set; }
    public DateOnly OccurrenceStartDate { get; set; }
    public DateOnly OccurrenceEndDate { get; set; }
}