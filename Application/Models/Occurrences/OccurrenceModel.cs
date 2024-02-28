using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Occurrences;

public class OccurrenceModel
{
    public int Id { get; set; }
    [NotNull] [MaxLength(200)] 
    public string Title { get; set; } = default!;
    [NotNull]
    public bool Active { get; set; }
}
