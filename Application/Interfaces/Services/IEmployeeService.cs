using Application.Models.Employees;

namespace Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeModel>> GetAll();
    Task<EmployeeModel> Create(EmployeeModel model);
    Task<EmployeeModel> Update(EmployeeModel model);
    Task<bool> Exists(int id);
}