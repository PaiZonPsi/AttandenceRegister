using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities;

namespace Application.Models.Attendances;

public class AttendanceModel
{
    public Employee Employee { get; set; }
    public Occurrence Occurrence { get; set; }
    public string? Description { get; set; }
    public DateOnly OccurrenceStartDate { get; set; }
    public DateOnly OccurrenceEndDate { get; set; }
}