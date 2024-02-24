using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class Attendance : BaseEntity
{
    public int Id { get; set; }
    public Employee Employee { get; set; }
    public Occurrence Occurrence { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [NotNull]
    public DateOnly OccurrenceStartDate { get; set; }
    [NotNull]
    public DateOnly OccurrenceEndDate { get; set; }
}