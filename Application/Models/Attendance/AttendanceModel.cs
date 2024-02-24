using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities;

namespace Application.Models.Attendance;

public class AttendanceModel
{
    public Domain.Entities.Employee Employee { get; set; }
    public Occurrence Occurrence { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [NotNull]
    public DateOnly OccurrenceStartDate { get; set; }
    [NotNull]
    public DateOnly OccurrenceEndDate { get; set; }
}