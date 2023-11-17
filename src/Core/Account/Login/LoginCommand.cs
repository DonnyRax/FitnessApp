using Domain.Abstractions;
using MediatR;

namespace Core.Account.Login;

public sealed record LoginCommand(string EmailAddress, string Password) : IRequest<Result<LoginResponse>>;
