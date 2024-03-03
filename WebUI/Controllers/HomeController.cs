using Application.Interfaces.Repository;
using Application.Models.Employees;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceRegister.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EmployeeModel> _validator;

    public HomeController(IEmployeeRepository repository, IMapper mapper, IValidator<EmployeeModel> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public IActionResult Index()
    {
        return View();
    }

    [AcceptVerbs("Get")]
    public async Task<IActionResult> GetEmployees([DataSourceRequest]DataSourceRequest request)
    {
        var employees = await _repository.GetAllAsync();
        var employeesDtos = _mapper.Map<IEnumerable<EmployeeModel>>(employees);
        var result = await employeesDtos.ToDataSourceResultAsync(request);
        
        return Json(result);
    }

    public async Task<IActionResult> PostEmployee(EmployeeModel employeeModel, [DataSourceRequest] DataSourceRequest request)
    {
        var result = await _validator.ValidateAsync(employeeModel);
        
        if (result.IsValid == false)
        {
            result.AddToModelState(this.ModelState);
        }
        
        var employeeEntity = _mapper.Map<Employee>(employeeModel);
        await _repository.CreateAsync(employeeEntity);
        await _repository.SaveChangesAsync();
        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask, ModelState);
    }

    public async Task<IActionResult> PutEmployee(EmployeeModel employeeModel, [DataSourceRequest] DataSourceRequest request)
    {
        var entityToUpdate = await _repository.GetByIdAsync(employeeModel.Id);

        if (entityToUpdate == null)
            return BadRequest();
        
        _repository.UpdateEntity(_mapper.Map<Employee>(employeeModel));
        
        if (await _repository.SaveChangesAsync() == true)
        {
            var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
            return Json(await dataSourceResultTask, ModelState);
        }
        
        return BadRequest();
    }
}