using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Employees;
using AttendanceRegister.Factories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Services;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceRegister.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EmployeeModel> _validator;
    private readonly IContentResultFactory _contentResultFactory;
    private readonly IEmployeeService _employeeService;

    public HomeController(IEmployeeRepository repository,
        IMapper mapper,
        IValidator<EmployeeModel> validator,
        IContentResultFactory contentResultFactory,
        IEmployeeService employeeService)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _contentResultFactory = contentResultFactory;
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetEmployees([DataSourceRequest] DataSourceRequest request)
    {
        var employees = await _employeeService.GetAll();
        return await _contentResultFactory.CreateReadOnlyContentResult(employees, request);
    }

    public async Task<IActionResult> PostEmployee([DataSourceRequest] DataSourceRequest request,
        [FromForm] EmployeeModel employeeModel)
    {
        var validationResult = await _validator.ValidateAsync(employeeModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            return await _contentResultFactory.CreateContentResult(employeeModel, request, ModelState);
        }

        var employee = await _employeeService.Create(employeeModel);
        return await _contentResultFactory
            .CreateContentResult(_mapper.Map<EmployeeModel>(employee), request, ModelState);
    }

    public async Task<IActionResult> PutEmployee([DataSourceRequest] DataSourceRequest request,
        [FromForm] EmployeeModel employeeModel)
    {
        var validationResult = await _validator.ValidateAsync(employeeModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            return await _contentResultFactory.CreateContentResult(employeeModel, request, ModelState);
        }

        if (await _repository.EntityExists(employeeModel.Id) == false)
            return BadRequest();
        var updatedModel = await _employeeService.Update(employeeModel);
        return await _contentResultFactory.CreateContentResult(updatedModel,
            request,
            ModelState);
    }
}