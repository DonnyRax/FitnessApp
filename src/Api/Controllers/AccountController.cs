using API.Account.Login;
using API.Account.Register;
using API.DTOs;
using Core.Account.Login;
using Core.Account.Register;
using Core.Security.Token;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AccountController(IMediator mediator, 
        UserManager<User> userManager, 
        ITokenService tokenService,
        IValidator<LoginRequest> loginValidator, 
        IValidator<RegisterRequest> registerValidator, 
        ILogger<AccountController> logger)
    {
        _logger = logger;
        _mediator = mediator;
        _userManager = userManager;
        _tokenService = tokenService;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }

    // [AllowAnonymous]
    // [HttpPost("login")]
    // public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    // {
    //     var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);
    //     if(!validationResult.IsValid)
    //         return BadRequest(validationResult.Errors);

    //     var command = new LoginCommand(request.EmailAddress, request.Password);
    //     var result = await _mediator.Send(command, cancellationToken);

    //     if(result.IsFailure)
    //     {
    //         return NotFound(result.Error);
    //     }

    //     return Ok(result.Value);
    // }

    // [AllowAnonymous]
    // [HttpPost("register")]
    // public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request, CancellationToken cancellationToken)
    // {
    //     var validationResult = await _registerValidator.ValidateAsync(request, cancellationToken);
    //     if(!validationResult.IsValid)
    //         return BadRequest(validationResult.Errors);

    //     var command = new RegisterCommand(request.DisplayName, request.EmailAddress, request.Password);
    //     var result = await _mediator.Send(command, cancellationToken);
        
    //     if(result.IsFailure)
    //     {
    //         return BadRequest(result.Error);
    //     }
        
    //     return Ok(result.Value);
    // }

    [HttpGet("currentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        var token = await _tokenService.GenerateToken(user);
        return new UserDto(user.DisplayName, token);
    }
}
