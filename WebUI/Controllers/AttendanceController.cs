using Application.Interfaces.Repository;
using Application.Models.Attendances;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceRegister.Controllers;

public class AttendanceController : Controller
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<AttendanceModel> _validator;

    public AttendanceController(IAttendanceRepository repository, IMapper mapper,
        IValidator<AttendanceModel> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ActionResult> GetAttendances([DataSourceRequest] DataSourceRequest request)
    {
        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask);
    }

    public async Task<ActionResult<AttendanceModel>> PostAttendance([DataSourceRequest] DataSourceRequest request, AttendanceModel model)
    {
        var result = await _validator.ValidateAsync(model);

        if (result.IsValid == false)
        {
            result.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var attendanceEntity = _mapper.Map<Attendance>(model);
        await _repository.CreateAsync(attendanceEntity);
        await _repository.SaveChangesAsync();

        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask);
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

        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask);
    }
}