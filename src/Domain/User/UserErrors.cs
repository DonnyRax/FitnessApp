using Domain.Abstractions;

namespace Domain.User;

public class UserErrors
{
    public static Error UserNotFound = new("User.NotFound", "User not found");
}
