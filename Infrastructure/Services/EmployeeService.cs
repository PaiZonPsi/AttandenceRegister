using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Employees;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;
    
    public EmployeeService(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<EmployeeModel>> GetAll()
    {
        var employees = await _repository.GetAllAsync();
        var employeesDtos = _mapper.Map<IEnumerable<EmployeeModel>>(employees);
        return employeesDtos;
    }
    
    public async Task<EmployeeModel> Create(EmployeeModel model)
    {
        var employeeEntity = new Employee(model.FirstName, model.LastName, model.Email, model.PhoneNumber);
        await _repository.CreateAsync(employeeEntity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<EmployeeModel>(employeeEntity);
    }
    
    public async Task<EmployeeModel> Update(EmployeeModel model)
    {
        var entityToUpdate = await _repository.GetByIdAsync(model.Id);
        entityToUpdate!.SetFirstName(model.FirstName);
        entityToUpdate.SetLastName(model.LastName);
        entityToUpdate.SetEmail(model.Email);
        entityToUpdate.SetPhoneNumber(model.PhoneNumber);
        await _repository.SaveChangesAsync();
        return _mapper.Map<EmployeeModel>(entityToUpdate);
    }
}