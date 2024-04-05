using AttendanceRegister.Factories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AttendanceRegister.Controllers;

public class EmployeeSummaryController : Controller
{
    private readonly ISummaryRepository _repository;
    private readonly IContentResultFactory _contentResultFactory;
    
    public EmployeeSummaryController(ISummaryRepository repository, IContentResultFactory contentResultFactory)
    {
        _repository = repository;
        _contentResultFactory = contentResultFactory;
    }
    
    public IActionResult EmployeeSummaryView()
    {
        return View();
    }

    public async Task<IActionResult> GetEmployeeSummary([DataSourceRequest] DataSourceRequest request, int? employeeId)
    {
        if (employeeId.HasValue == false)
            return BadRequest();
        var results = _repository.GetSummaryForEmployee(employeeId.Value).ToList();
        return await _contentResultFactory.CreateReadOnlyContentResult(results, request);
    }
}