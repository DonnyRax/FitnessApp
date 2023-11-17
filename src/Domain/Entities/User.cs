using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string DisplayName { get; set; }
    public virtual List<Challenge> SignedUpChallenges { get; set; }
}
