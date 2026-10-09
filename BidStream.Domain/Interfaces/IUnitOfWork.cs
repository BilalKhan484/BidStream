using BidStream.Domain.Entities;


namespace BidStream.Domain.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IRepository<Item> Item { get; }

        IRepository<Bid> Bid { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
