namespace Domain.Entities;

public class WeightHistory
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double WeightInKg { get; set; }
    public DateOnly Created { get; set; }
}
