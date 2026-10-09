using BidStream.Domain.Entities;
using BidStream.Domain.Enums;
using BidStream.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidStream.Application.Features.Auctions.Commands
{
        public record AuctionCommand(
                string Title,
                string? Description,
                decimal StartingPrice,
                DateTime StartTime,
                DateTime EndTime,
                string SellerId
            ) : IRequest<Guid>;
    public  class CreateAuctionCommandHandler : IRequestHandler<AuctionCommand, Guid>
    {
        
        private readonly IUnitOfWork _unitofWork;

        public CreateAuctionCommandHandler(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<Guid> Handle(AuctionCommand request, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Title = request.Title,
                Description = request.Description,
                StartingPrice = request.StartingPrice,
                CurrentPrice = request.StartingPrice,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = request.StartTime <= DateTime.UtcNow ? AuctionStatus.Active : AuctionStatus.NotStarted,
                SellerId = request.SellerId
            };
        
            await _unitofWork.Item.AddAsync(item, cancellationToken);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
