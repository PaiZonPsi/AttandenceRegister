using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class Attendance : BaseEntity
{
    [NotNull]
    public int? EmployeeId { get; private set; }
    public Employee? Employee { get; private set; }
    [NotNull]
    public int? OccurrenceId { get; private set; }
    public Occurrence? Occurrence { get; private set; }
    [MaxLength(500)]
    public string? Description { get; private set; }
    [NotNull]
    public DateOnly OccurrenceStartDate { get; private set; }
    [NotNull]
    public DateOnly OccurrenceEndDate { get; private set; }

    public void SetEmployeeId(int value) => EmployeeId = value;
    public void SetOccurrenceId(int value) => OccurrenceId = value;
    public void SetDescription(string? value) => Description = value;
    public void SetOccurrenceStartDate(DateOnly value) => OccurrenceStartDate = value;
    public void SetOccurrenceEndDate(DateOnly value) => OccurrenceEndDate = value;
}