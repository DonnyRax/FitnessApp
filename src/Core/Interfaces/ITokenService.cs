using Domain.Entities;

namespace Core.Security.Token;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}
