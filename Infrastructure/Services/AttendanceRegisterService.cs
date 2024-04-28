using System.Diagnostics;
using Application.Interfaces.Repository;
using Application.Models.Attendances;
using Application.Models.Employees;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class AttendanceRegisterService : IAttendanceRegisterService
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;
    
    public AttendanceRegisterService(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<AttendanceModel>> GetAll()
    {
        var attendances = await _repository.GetAllAsync();
        var attendancesDtos = _mapper.Map<IEnumerable<AttendanceModel>>(attendances);
        return attendancesDtos;
    }
    
    public async Task<AttendanceModel> Create(AttendanceModel model)
    {
        var attendanceEntity = new Attendance();
        attendanceEntity.SetEmployeeId(model.Employee!.Id);
        attendanceEntity.SetOccurrenceId(model.Occurrence!.Id);
        attendanceEntity.SetDescription(model.Description);
        attendanceEntity.SetOccurrenceStartDate(DateOnly.FromDateTime(model.OccurrenceStartDate));
        attendanceEntity.SetOccurrenceEndDate(DateOnly.FromDateTime(model.OccurrenceEndDate));
        await _repository.CreateAsync(attendanceEntity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<AttendanceModel>(attendanceEntity);
    }
    
    public async Task<AttendanceModel> Update(AttendanceModel model)
    {
        var entityToUpdate = await _repository.GetByIdAsync(model.Id);
        
        if (entityToUpdate == null) return null!;
        
        entityToUpdate.SetEmployeeId(model.Employee!.Id);
        entityToUpdate.SetOccurrenceId(model.Occurrence!.Id);
        entityToUpdate.SetDescription(model.Description);
        entityToUpdate.SetOccurrenceStartDate(DateOnly.FromDateTime(model.OccurrenceStartDate));
        entityToUpdate.SetOccurrenceEndDate(DateOnly.FromDateTime(model.OccurrenceEndDate));
        
        await _repository.SaveChangesAsync();
        return _mapper.Map<AttendanceModel>(entityToUpdate);
    }

    public async Task Delete(AttendanceModel model)
    {
        _repository.Remove(_mapper.Map<Attendance>(model));
    }

    public async Task<bool> Exists(int id)
    {
        return await _repository.EntityExists(id);
    }
}

public interface IAttendanceRegisterService
{
    Task<IEnumerable<AttendanceModel>> GetAll();
    Task<AttendanceModel> Create(AttendanceModel model);
    Task<AttendanceModel> Update(AttendanceModel model);
    Task Delete(AttendanceModel model);
    Task<bool> Exists(int id);
}