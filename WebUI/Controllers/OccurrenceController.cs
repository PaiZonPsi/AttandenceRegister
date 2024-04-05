using Application.Interfaces.Repository;
using AttendanceRegister.Factories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Services;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using OccurrenceModel = Application.Models.Occurrences.OccurrenceModel;

namespace AttendanceRegister.Controllers;

public class OccurrenceController : Controller
{
    private readonly IOccurrenceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<OccurrenceModel> _validator;
    private readonly IContentResultFactory _contentResultFactory;
    public OccurrenceController(IOccurrenceRepository repository, IMapper mapper, IValidator<OccurrenceModel> validator, IContentResultFactory contentResultFactory)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _contentResultFactory = contentResultFactory;
    }
    
    public IActionResult Occurrences()
    {
        return View();
    }
    
    public async Task<ActionResult> GetOccurrences([DataSourceRequest] DataSourceRequest request)
    {
        var occurrences = await _repository.GetAllAsync();
        var employeesDtos = _mapper.Map<IEnumerable<OccurrenceModel>>(occurrences);
        return await _contentResultFactory.CreateReadOnlyContentResult(employeesDtos, request);
    }

    public async Task<ActionResult> PostOccurrence([DataSourceRequest] DataSourceRequest request, [FromForm] OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            return await _contentResultFactory.CreateContentResult(occurrenceModel, request, ModelState);
        }

        var occurrenceEntity = new Occurrence(occurrenceModel.Title, occurrenceModel.Active);
        await _repository.CreateAsync(occurrenceEntity);
        await _repository.SaveChangesAsync();
        return await _contentResultFactory
            .CreateContentResult(_mapper.Map<OccurrenceModel>(occurrenceEntity), request, ModelState);
    }

    public async Task<ActionResult> PutOccurrence([DataSourceRequest] DataSourceRequest request, [FromForm] OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            return await _contentResultFactory.CreateContentResult(occurrenceModel, request, ModelState);
        }

        var entityToUpdate = await _repository.GetByIdAsync(occurrenceModel.Id);

        if (entityToUpdate == null)
            return BadRequest();

        entityToUpdate.SetTitle(occurrenceModel.Title);
        entityToUpdate.SetActivity(occurrenceModel.Active);
        await _repository.SaveChangesAsync();
        return await _contentResultFactory
            .CreateContentResult(_mapper.Map<OccurrenceModel>(entityToUpdate), request, ModelState);
    }
}