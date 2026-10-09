using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BidStream.Domain.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<T> GetByIdAsync(Guid id , CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(T entity);
    }
    {
    }
}
