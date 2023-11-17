using System.ComponentModel.DataAnnotations;

namespace API.Account.Login;

public sealed class LoginRequest
{
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}
