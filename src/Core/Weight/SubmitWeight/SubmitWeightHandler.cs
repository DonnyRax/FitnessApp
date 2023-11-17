using Domain.Abstractions;
using Domain.Entities;
using Domain.User;
using Domain.Weight;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Data;
using Core.Interfaces.Clock;

namespace Core.Weight.SubmitWeight;

public class SubmitWeightHandler : IRequestHandler<SubmitWeightCommand, Result<SubmitWeightResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SubmitWeightHandler(UserManager<User> userManager,
            AppDbContext dbContext,
            IDateTimeProvider dateTimeProvider)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<SubmitWeightResponse>> Handle(SubmitWeightCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if(user is null)
            return Result.Failure<SubmitWeightResponse>(UserErrors.UserNotFound);

        var weightHistory = new WeightHistory {
            UserId = request.Id,
            WeightInKg = request.Weight,
            Created = DateOnly.FromDateTime(_dateTimeProvider.UtcNow)
        };

        await _dbContext.WeightHistory.AddAsync(weightHistory, cancellationToken);

        var save = await _dbContext.SaveChangesAsync(cancellationToken);
        if(save > 0)
            return new SubmitWeightResponse(Guid.NewGuid(), Guid.NewGuid(), 80.0);

        return Result.Failure<SubmitWeightResponse>(WeightErrors.SubmitWeightFailed);
    }
}
