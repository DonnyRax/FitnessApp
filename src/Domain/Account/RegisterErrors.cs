using Domain.Abstractions;

namespace Domain.Account;

public class RegisterErrors
{
    public static Error Failed = new(
    "Register.Failed",
    "Registration failed. Check details and try again.");
}
