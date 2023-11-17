using API.Weight.SubmitWeight;
using Core.Weight.SubmitWeight;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WeightController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IValidator<SubmitWeightRequest> _submitWeightRequestValidator;
    private readonly ILogger<WeightController> _logger;

    public WeightController(IMediator mediator, 
            IValidator<SubmitWeightRequest> submitWeightRequestValidator, ILogger<WeightController> logger)
    {
        _mediator = mediator;
        _submitWeightRequestValidator = submitWeightRequestValidator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<SubmitWeightResponse>> SubmitWeight(SubmitWeightRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _submitWeightRequestValidator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new SubmitWeightCommand(request.Id, request.Weight);
        var result = await _mediator.Send(command, cancellationToken);
        
        if(result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}
