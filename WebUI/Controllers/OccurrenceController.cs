using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OccurrenceModel = Application.Models.Occurrences.OccurrenceModel;

namespace AttendanceRegister.Controllers;

public class OccurrenceController : Controller
{
    private readonly IOccurrenceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<OccurrenceModel> _validator;

    public OccurrenceController(IOccurrenceRepository repository, IMapper mapper, IValidator<OccurrenceModel> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public IActionResult Occurrences()
    {
        return View();
    }
    
    public async Task<ActionResult> GetOccurrences([DataSourceRequest] DataSourceRequest request)
    {
        var occurrences = await _repository.GetAllAsync();
        var employeesDtos = _mapper.Map<IEnumerable<OccurrenceModel>>(occurrences);
        var dataSourceResult = await employeesDtos.ToDataSourceResultAsync(request);
        return new ContentResult() {Content = JsonConvert.SerializeObject(dataSourceResult), ContentType = "application/json"};
    }

    public async Task<ActionResult> PostOccurrence([DataSourceRequest] DataSourceRequest request, [FromForm] OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            var errorResult = await new List<OccurrenceModel> { occurrenceModel }.ToDataSourceResultAsync(request, ModelState);
            return new ContentResult() {Content = JsonConvert.SerializeObject(errorResult), ContentType = "application/json"};
        }

        var occurrenceEntity = new Occurrence(occurrenceModel.Title, occurrenceModel.Active);
        await _repository.CreateAsync(occurrenceEntity);
        await _repository.SaveChangesAsync();
        var dataSourceResult = await new List<OccurrenceModel> {_mapper.Map<OccurrenceModel>(occurrenceEntity)}.ToDataSourceResultAsync(request);
        return new ContentResult()
            { Content = JsonConvert.SerializeObject(dataSourceResult), ContentType = "application/json" };
        
    }

    public async Task<ActionResult> PutOccurrence([DataSourceRequest] DataSourceRequest request, [FromForm] OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            var errorResult = await new List<OccurrenceModel> { occurrenceModel }.ToDataSourceResultAsync(request, ModelState);
            return new ContentResult() {Content = JsonConvert.SerializeObject(errorResult), ContentType = "application/json"};
        }

        var entityToUpdate = await _repository.GetByIdAsync(occurrenceModel.Id);

        if (entityToUpdate == null)
            return BadRequest();

        entityToUpdate.SetTitle(occurrenceModel.Title);
        entityToUpdate.SetActivity(occurrenceModel.Active);
        await _repository.SaveChangesAsync();

        var dataSourceResult = await new List<OccurrenceModel> {_mapper.Map<OccurrenceModel>(entityToUpdate)}.ToDataSourceResultAsync(request);
        return new ContentResult()
            { Content = JsonConvert.SerializeObject(dataSourceResult), ContentType = "application/json" };

    }
}