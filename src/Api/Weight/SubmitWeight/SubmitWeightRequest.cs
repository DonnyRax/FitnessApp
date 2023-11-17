using System.ComponentModel.DataAnnotations;

namespace API.Weight.SubmitWeight;

public class SubmitWeightRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public double Weight { get; set; }
}
