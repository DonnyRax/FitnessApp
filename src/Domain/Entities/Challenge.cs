namespace Domain.Entities;

public class Challenge
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsPrivate { get; set; }
    public virtual List<User> SignedUpUsers { get; set; }
}
