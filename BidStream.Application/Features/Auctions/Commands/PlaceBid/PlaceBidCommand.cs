using MediatR;
using BidStream.Domain.Entities;
using BidStream.
using BidStream.Domain.Enums;



namespace BidStream.Application.Features.Auctions.Commands.PlaceBid
{
    public static class PlaceBidCommand
    {
        //Command Request

        public record Command(Guid ItemId , decimal BidAmount, string BidderId) : MediatR.IRequest<Unit>;


        //Handler

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.Items.FindAsync(new object[] { request.ItemId } , cancellationToken);

                Console.WriteLine($"Placing bid of {request.BidAmount} on item {request.ItemId} by bidder {request.BidderId}");
                return Unit.Value;
            }
        }
    }
}
