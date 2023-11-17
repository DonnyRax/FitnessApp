using Domain.Abstractions;

namespace Domain.Account;

public class LoginErrors
{
    public static Error NotFound = new(
    "Login.NotFound",
    "Login failed. Check username and password and try again.");
}
