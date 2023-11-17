using Domain.Abstractions;
using MediatR;

namespace Core.Account.Register;

public sealed record RegisterCommand(string DisplayName, string EmailAddress, string Password) : IRequest<Result<RegisterResponse>>;

