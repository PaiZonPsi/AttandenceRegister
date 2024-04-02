using Infrastructure.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AttendanceRegister.Controllers;

public class EmployeeSummaryController : Controller
{
    private readonly ISummaryRepository _repository;
    public EmployeeSummaryController(ISummaryRepository repository)
    {
        _repository = repository;
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
        var dataSourceResult = await results.ToDataSourceResultAsync(request);
        var serializeObject = JsonConvert.SerializeObject(dataSourceResult);
        return new ContentResult() {Content = serializeObject, ContentType = "application/json"};
    }
}