using Domain.Abstractions;

namespace Domain.Weight;

public class WeightErrors
{
    public static Error SubmitWeightFailed = new Error(
        "SubmitWeight.SaveError",
        "Failed to save weight"
    );
}
