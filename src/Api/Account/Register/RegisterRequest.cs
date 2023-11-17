using System.ComponentModel.DataAnnotations;

namespace API.Account.Register;

public sealed class RegisterRequest
{
    [Required]
    public string DisplayName { get; set; }
    
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}