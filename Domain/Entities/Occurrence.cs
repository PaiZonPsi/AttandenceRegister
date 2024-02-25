using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class Occurrence : BaseEntity
{
    [NotNull] [MaxLength(200)] 
    public string Title { get; set; } = default!;
    [NotNull]
    public bool Active { get; set; }
}