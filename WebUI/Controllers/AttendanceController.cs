using Application.Interfaces.Services;
using Application.Models.Attendances;
using AttendanceRegister.Factories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Services;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceRegister.Controllers;

public class AttendanceController : Controller
{
    private readonly IAttendanceRegisterService _attendanceRegisterService;
    private readonly IValidator<AttendanceModel> _validator;
    private readonly IContentResultFactory _contentResultFactory;
    public AttendanceController(IValidator<AttendanceModel> validator, 
        IContentResultFactory contentResultFactory, 
        IAttendanceRegisterService attendanceRegisterService)
    {
        _validator = validator;
        _contentResultFactory = contentResultFactory;
        _attendanceRegisterService = attendanceRegisterService;
    }

    public IActionResult Attendances()
    {
        return View();
    }
    
    public async Task<ActionResult> GetAttendances([DataSourceRequest] DataSourceRequest request)
    {
        var attendancesDtos = await _attendanceRegisterService.GetAll();
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
        
        var attendanceModel = await _attendanceRegisterService.Create(model);
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

        if (await _attendanceRegisterService.Exists(model.Id) == false)
            return BadRequest();
        
        var entity = await _attendanceRegisterService.Update(model);
        
        return await _contentResultFactory.CreateContentResult(entity, request, ModelState);
    }
    
    public async Task<ActionResult> DeleteAttendance([DataSourceRequest] DataSourceRequest request, AttendanceModel model)
    {
        if (await _attendanceRegisterService.Exists(model.Id) == false)
            return BadRequest();

        await _attendanceRegisterService.Delete(model);

        var attendances = await _attendanceRegisterService.GetAll();
        return await _contentResultFactory.CreateReadOnlyContentResult(attendances, request);
    }
}