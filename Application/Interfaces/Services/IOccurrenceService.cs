using Application.Models.Occurrences;

namespace Application.Interfaces.Services;

public interface IOccurrenceService
{
    Task<IEnumerable<OccurrenceModel>> GetAll();
    Task<OccurrenceModel> Create(OccurrenceModel model);
    Task<OccurrenceModel> Update(OccurrenceModel model);
    Task<bool> Exists(int id);
}