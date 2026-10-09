using FluentValidation;

namespace BidStream.Application.Features.Auctions.Commands;

public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.StartingPrice).GreaterThan(0);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
        RuleFor(x => x.SellerId).NotEmpty();
    }
}