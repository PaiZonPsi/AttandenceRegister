using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AttendanceRegister.Factories;

public interface IContentResultFactory
{
    Task<ContentResult> CreateContentResult<T>(T entity, DataSourceRequest request,
        ModelStateDictionary modelState);

    Task<ContentResult> CreateContentResult<T>(IEnumerable<T> collection, DataSourceRequest request,
        ModelStateDictionary modelState);
 
    Task<ContentResult> CreateReadOnlyContentResult<T>(IEnumerable<T> collection, DataSourceRequest request);
}