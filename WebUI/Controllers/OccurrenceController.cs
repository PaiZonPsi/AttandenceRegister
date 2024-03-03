using Application.Interfaces.Repository;
using Application.Models.Occurrences;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

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
        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask);
    }

    public async Task<ActionResult> PostOccurrence([DataSourceRequest] DataSourceRequest request, OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
        }
        
        var occurrenceEntity = _mapper.Map<Occurrence>(occurrenceModel);
        await _repository.CreateAsync(occurrenceEntity);
        await _repository.SaveChangesAsync();
        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask, ModelState);
    }

    public async Task<ActionResult> PutOccurrence([DataSourceRequest] DataSourceRequest request, OccurrenceModel model)
    {
        var entityToUpdate = await _repository.GetByIdAsync(model.Id);

        if (entityToUpdate == null)
            return BadRequest();
        
        _repository.UpdateEntity(_mapper.Map<Occurrence>(model));
        await _repository.SaveChangesAsync();

        var dataSourceResultTask = (await _repository.GetAllAsync()).ToDataSourceResultAsync(request);
        return Json(await dataSourceResultTask, ModelState);

    }
}