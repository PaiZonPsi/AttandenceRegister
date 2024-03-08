using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Occurrences;

public class OccurrenceModel
{
    public int Id { get; set; }
    [Required] [MaxLength(200)] 
    public string Title { get; set; } = String.Empty;
    [Required]
    public bool Active { get; set; }
}
