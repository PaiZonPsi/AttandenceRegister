using Application.Interfaces.Repository;
using Application.Models.Attendances;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    public IActionResult Attendances()
    {
        return View();
    }
    
    public async Task<ActionResult> GetAttendances([DataSourceRequest] DataSourceRequest request)
    {
        var attendances = await _repository.GetAllAsync();
        var attendancesDtos = _mapper.Map<IEnumerable<AttendanceModel>>(attendances);
        var result = await attendancesDtos.ToDataSourceResultAsync(request);

        var serializeObject = JsonConvert.SerializeObject(result);
        return new ContentResult() {Content = serializeObject, ContentType = "application/json"};
    }

    public async Task<IActionResult> PostAttendance([DataSourceRequest] DataSourceRequest request, [FromForm] AttendanceModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(this.ModelState);
            var errorResult = await new List<AttendanceModel> { model }.ToDataSourceResultAsync(request, ModelState);
            return new ContentResult() {Content = JsonConvert.SerializeObject(errorResult), ContentType = "application/json"};
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
        var result = await new List<AttendanceModel> { attendanceModel }.ToDataSourceResultAsync(request);


        return new ContentResult() {Content = JsonConvert.SerializeObject(result), ContentType = "application/json"};
    }
    
    public async Task<IActionResult> PutAttendance([DataSourceRequest] DataSourceRequest request, [FromForm] AttendanceModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(this.ModelState);
            var errorResult = await new List<AttendanceModel> { model }.ToDataSourceResultAsync(request, ModelState);
            return new ContentResult() {Content = JsonConvert.SerializeObject(errorResult), ContentType = "application/json"};
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
            var dataSourceResult = await new List<AttendanceModel>(){_mapper.Map<AttendanceModel>(entityToUpdate)}.ToDataSourceResultAsync(request);
            return new ContentResult() {Content = JsonConvert
                .SerializeObject(dataSourceResult), ContentType = "application/json"};
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

        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask);
    }
}