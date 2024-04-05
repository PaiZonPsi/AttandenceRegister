using Application.Interfaces.Repository;
using Application.Models.Attendances;
using AttendanceRegister.Factories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Services;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceRegister.Controllers;

public class AttendanceController : Controller
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<AttendanceModel> _validator;
    private readonly IContentResultFactory _contentResultFactory;
    public AttendanceController(IAttendanceRepository repository, IMapper mapper,
        IValidator<AttendanceModel> validator, IContentResultFactory contentResultFactory)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _contentResultFactory = contentResultFactory;
    }

    public IActionResult Attendances()
    {
        return View();
    }
    
    public async Task<ActionResult> GetAttendances([DataSourceRequest] DataSourceRequest request)
    {
        var attendances = await _repository.GetAllAsync();
        var attendancesDtos = _mapper.Map<IEnumerable<AttendanceModel>>(attendances);
        return await _contentResultFactory.CreateReadOnlyContentResult(attendancesDtos, request);
    }

    public async Task<IActionResult> PostAttendance([DataSourceRequest] DataSourceRequest request, [FromForm] AttendanceModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(this.ModelState);
            return await _contentResultFactory.CreateContentResult(model, request, ModelState);
        }
        
        var attendanceEntity = new Attendance();
        attendanceEntity.SetEmployeeId(model.Employee!.Id);
        attendanceEntity.SetOccurrenceId(model.Occurrence!.Id);
        attendanceEntity.SetDescription(model.Description);
        attendanceEntity.SetOccurrenceStartDate(DateOnly.FromDateTime(model.OccurrenceStartDate));
        attendanceEntity.SetOccurrenceEndDate(DateOnly.FromDateTime(model.OccurrenceEndDate));
        await _repository.CreateAsync(attendanceEntity);
        await _repository.SaveChangesAsync();
        var attendanceModel = _mapper.Map<AttendanceModel>(attendanceEntity);
        attendanceModel.Employee = model.Employee;
        attendanceModel.Occurrence = model.Occurrence;
        return await _contentResultFactory.CreateContentResult(attendanceModel, request, ModelState);
    }
    
    public async Task<IActionResult> PutAttendance([DataSourceRequest] DataSourceRequest request, [FromForm] AttendanceModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(this.ModelState);
            return await _contentResultFactory.CreateContentResult(model, request, ModelState);
        }
        var entityToUpdate = await _repository.GetByIdAsync(model.Id);

        if (entityToUpdate == null)
            return BadRequest();
        entityToUpdate.SetEmployeeId(model.Employee!.Id);
        entityToUpdate.SetOccurrenceId(model.Occurrence!.Id);
        entityToUpdate.SetDescription(model.Description);
        entityToUpdate.SetOccurrenceStartDate(DateOnly.FromDateTime(model.OccurrenceStartDate));
        entityToUpdate.SetOccurrenceEndDate(DateOnly.FromDateTime(model.OccurrenceEndDate));
        
        if (await _repository.SaveChangesAsync() == true)
        {
            return await _contentResultFactory
                .CreateContentResult(_mapper.Map<AttendanceModel>(entityToUpdate), request, ModelState);
        }
        
        return BadRequest();
    }
    
    public async Task<ActionResult> DeleteAttendance([DataSourceRequest] DataSourceRequest request, AttendanceModel model)
    {
        var entity = await _repository.GetByIdAsync(model.Id);

        if (entity == null)
            return BadRequest();
        
        _repository.Remove(entity);
        if (await _repository.SaveChangesAsync() == false)
        {
            return BadRequest();
        }

        var attendances = await _repository.GetAllAsync();
        return await _contentResultFactory.CreateReadOnlyContentResult(attendances, request);
    }
}