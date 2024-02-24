using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class Occurrence : BaseEntity
{
    public int Id { get; set; }
    [NotNull] [MaxLength(200)] 
    public string Title { get; set; } = default!;
    [NotNull]
    public bool Active { get; set; }
}