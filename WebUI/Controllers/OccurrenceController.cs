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
    private readonly IOccurrenceService _occurrenceService;
    private readonly IValidator<OccurrenceModel> _validator;
    private readonly IContentResultFactory _contentResultFactory;
    
    public OccurrenceController(IValidator<OccurrenceModel> validator, 
        IContentResultFactory contentResultFactory, 
        IOccurrenceService occurrenceService)
    {
        _validator = validator;
        _contentResultFactory = contentResultFactory;
        _occurrenceService = occurrenceService;
    }
    
    public IActionResult Occurrences()
    {
        return View();
    }
    
    public async Task<ActionResult> GetOccurrences([DataSourceRequest] DataSourceRequest request)
    {
        var occurrences = await _occurrenceService.GetAll();
        return await _contentResultFactory.CreateReadOnlyContentResult(occurrences, request);
    }

    public async Task<ActionResult> PostOccurrence([DataSourceRequest] DataSourceRequest request, 
        [FromForm] OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            return await _contentResultFactory.CreateContentResult(occurrenceModel, request, ModelState);
        }
        
        var model = await _occurrenceService.Create(occurrenceModel);
        return await _contentResultFactory
            .CreateContentResult(model, request, ModelState);
    }

    public async Task<ActionResult> PutOccurrence([DataSourceRequest] DataSourceRequest request, 
        [FromForm] OccurrenceModel occurrenceModel)
    {
        var validationResult = await _validator.ValidateAsync(occurrenceModel);
        
        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
            return await _contentResultFactory.CreateContentResult(occurrenceModel, request, ModelState);
        }

        if (await _occurrenceService.Exists(occurrenceModel.Id) == false)
            return BadRequest();

        var model = await _occurrenceService.Update(occurrenceModel);
        return await _contentResultFactory
            .CreateContentResult(model, request, ModelState);
    }
}