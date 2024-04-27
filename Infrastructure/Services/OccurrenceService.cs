using Application.Interfaces.Repository;
using Application.Models.Occurrences;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class OccurrenceService : IOccurrenceService
{
    private readonly IOccurrenceRepository _repository;
    private readonly IMapper _mapper;

    public OccurrenceService(IOccurrenceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<OccurrenceModel>> GetAll()
    {
        var employees = await _repository.GetAllAsync();
        var employeesDtos = _mapper.Map<IEnumerable<OccurrenceModel>>(employees);
        return employeesDtos;
    }
    
    public async Task<OccurrenceModel> Create(OccurrenceModel model)
    {
        var employeeEntity = new Occurrence(model.Title, model.Active);
        await _repository.CreateAsync(employeeEntity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<OccurrenceModel>(employeeEntity);
    }
    
    public async Task<OccurrenceModel> Update(OccurrenceModel model)
    {
        var entityToUpdate = await _repository.GetByIdAsync(model.Id);
        entityToUpdate!.SetTitle(model.Title);
        entityToUpdate.SetActivity(model.Active);
        await _repository.SaveChangesAsync();
        return _mapper.Map<OccurrenceModel>(entityToUpdate);
    }
    
    public async Task<bool> Exists(int id)
    {
        return await _repository.EntityExists(id);
    }
}

public interface IOccurrenceService
{
    Task<IEnumerable<OccurrenceModel>> GetAll();
    Task<OccurrenceModel> Create(OccurrenceModel model);
    Task<OccurrenceModel> Update(OccurrenceModel model);
    Task<bool> Exists(int id);
}