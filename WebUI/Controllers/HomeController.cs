using Application.Interfaces.Repository;
using Application.Models.Employees;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        
        return new ContentResult() {Content = JsonConvert.SerializeObject(result), ContentType = "application/json"};
    }

    public async Task<IActionResult> PostEmployee([DataSourceRequest] DataSourceRequest request, EmployeeModel employeeModel)
    {
        //temp solution telerik return all the values from grid 
        if (employeeModel.Id != 0) return BadRequest();
        var validationResult = await _validator.ValidateAsync(employeeModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(this.ModelState);
        }
        
        var employeeEntity = new Employee(employeeModel.FirstName, employeeModel.LastName, employeeModel.Email, employeeModel.PhoneNumber);
        var result = await new List<EmployeeModel> { _mapper.Map<EmployeeModel>(employeeEntity) }.ToDataSourceResultAsync(request);
        await _repository.CreateAsync(employeeEntity);
        await _repository.SaveChangesAsync();
        return new ContentResult() {Content = JsonConvert.SerializeObject(result), ContentType = "application/json"};
    }

    public async Task<IActionResult> PutEmployee(EmployeeModel employeeModel, [DataSourceRequest] DataSourceRequest request)
    {
        var entityToUpdate = await _repository.GetByIdAsync(employeeModel.Id);

        if (entityToUpdate == null)
            return BadRequest();
        entityToUpdate.SetFirstName(employeeModel.FirstName);
        entityToUpdate.SetLastName(employeeModel.LastName);
        entityToUpdate.SetEmail(employeeModel.Email);
        entityToUpdate.SetPhoneNumber(employeeModel.PhoneNumber);
        
        _repository.UpdateEntity(entityToUpdate);
        
        if (await _repository.SaveChangesAsync() == true)
        {
            var dataSourceResultTask = await (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
            return new ContentResult() {Content = JsonConvert.SerializeObject(_mapper.Map<EmployeeModel>(entityToUpdate)), ContentType = "application/json"};
        }
        
        return BadRequest();
    }
}