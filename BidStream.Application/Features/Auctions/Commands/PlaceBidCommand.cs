using BidStream.Domain.Entities;
using BidStream.Domain.Enums;
using BidStream.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static BidStream.Application.Features.Auctions.Commands.PlaceBid.PlaceBid;

namespace BidStream.Application.Features.Auctions.Commands.PlaceBid;


 public static class PlaceBid
{
    public record PlaceBidCommand(Guid ItemId, decimal Amount, string BidderId) : IRequest<Unit>;


    public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaceBidCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _unitOfWork.Items.GetByIdAsync(request.ItemId, cancellationToken);

            if (item == null)
                throw new KeyNotFoundException($"Auction item {request.ItemId} not found.");

            if (item.Status != AuctionStatus.Active)
                throw new InvalidOperationException("This auction is not currently active.");

            if (request.Amount <= item.CurrentPrice)
                throw new InvalidOperationException($"Bid amount must be greater than current price ({item.CurrentPrice}).");

            // 2. Create the bid
            var bid = new Bid
            {
                ItemId = item.Id,
                Amount = request.Amount,
                BidderId = request.BidderId,
                BidTime = DateTime.UtcNow
            };

            item.CurrentPrice = request.Amount;

            await _unitOfWork.Bid.AddAsync(bid, cancellationToken);
            _unitOfWork.Item.Update(item); 

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Bid was outpaced by another user. Please check the latest price and try again.");
            }

            return Unit.Value;
        }
    }
}
