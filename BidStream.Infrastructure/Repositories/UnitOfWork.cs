using BidStream.Domain.Entities;
using BidStream.Domain.Interfaces;
using BidStream.Infrastructure.Persistence;

namespace BidStream.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Item> _Item ;
        private IRepository<Bid> _Bid ;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Item> Item => _Item ??= new Repository<Item>(_context);

        public IRepository<Bid> Bid => _Bid ??= new Repository<Bid>(_context);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
