using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class Occurrence : BaseEntity
{
    [NotNull] [MaxLength(200)] 
    public string Title { get; private set; }
    [NotNull]
    public bool Active { get; private set; }

    public Occurrence(string title, bool active)
    {
        Title = title;
        Active = active;
    }

    public void SetTitle(string value) => Title = value;
    public void SetActivity(bool value) => Active = value;
}