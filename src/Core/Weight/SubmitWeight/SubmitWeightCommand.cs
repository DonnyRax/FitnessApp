using Domain.Abstractions;
using MediatR;

namespace Core.Weight.SubmitWeight;

public record SubmitWeightCommand(Guid Id, double Weight) : IRequest<Result<SubmitWeightResponse>>;
