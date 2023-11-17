using Core.Security.Token;
using Domain.Abstractions;
using Domain.Account;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Account.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    
    public LoginHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.EmailAddress);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Failure<LoginResponse>(LoginErrors.NotFound);
        }

        var token = await _tokenService.GenerateToken(user);
        return Result.Success(new LoginResponse(request.EmailAddress, token));
    }    
}
