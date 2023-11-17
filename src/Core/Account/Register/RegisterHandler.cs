using Domain.Abstractions;
using Domain.Account;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Account.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
        private readonly UserManager<User> _userManager;
    public RegisterHandler(UserManager<User> userManager)
    {
            _userManager = userManager;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User{
            Email = request.EmailAddress,
            UserName = request.EmailAddress,
            DisplayName = $"{request.DisplayName}"            
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if(!result.Succeeded)
        {
            return Result.Failure<RegisterResponse>(RegisterErrors.Failed);
        }

        await _userManager.AddToRoleAsync(user, "Member");

        return Result.Success(new RegisterResponse(request.DisplayName, request.EmailAddress));
    }
}
