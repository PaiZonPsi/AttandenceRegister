using System.ComponentModel.DataAnnotations;
using Application.Models.Employees;
using Application.Models.Occurrences;

namespace Application.Models.Attendances;

public class AttendanceModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    [UIHint("AttendanceEmployeeEditor")]
    public EmployeeModel? Employee { get; set; }
    public int OccurrenceId { get; set; }
    [UIHint("AttendanceOccurrenceEditor")]
    public OccurrenceModel? Occurrence { get; set; }
    public string? Description { get; set; }
    [UIHint("DatePickerGridEditor")]
    public DateTime OccurrenceStartDate { get; set; }
    [UIHint("DatePickerGridEditor")]
    public DateTime OccurrenceEndDate { get; set; }
}