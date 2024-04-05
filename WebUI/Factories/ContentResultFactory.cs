using Infrastructure.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AttendanceRegister.Factories;

public class ContentResultFactory : IContentResultFactory
{
    public async Task<ContentResult> CreateContentResult<T>(T entity, 
                                                            DataSourceRequest request,
                                                            ModelStateDictionary modelState)
    {
        return await CreateContentResult<T>(new List<T>{ entity }, request, modelState);
    }
    
    public async Task<ContentResult> CreateContentResult<T>(IEnumerable<T> collection, 
                                                            DataSourceRequest request, 
                                                            ModelStateDictionary modelState)
    {
        var dataSourceResult = await collection.ToDataSourceResultAsync(request, modelState);
        return new ContentResult() {Content = JsonConvert.SerializeObject(dataSourceResult), ContentType = "application/json"};
    }

    public async Task<ContentResult> CreateReadOnlyContentResult<T>(IEnumerable<T> collection, 
                                                                    DataSourceRequest request)
    {
        var dataSourceResult = await collection.ToDataSourceResultAsync(request);
        var serializeObject = JsonConvert.SerializeObject(dataSourceResult);
        return new ContentResult() {Content = serializeObject, ContentType = "application/json"};
    }
}