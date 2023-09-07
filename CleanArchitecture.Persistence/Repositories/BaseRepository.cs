using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Get(Guid id, CancellationToken cancellationToken) => await _dbContext.Set<T>()
                                                                                                  .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


        public async Task<List<T>> GetAll(CancellationToken cancellationToken) => await _dbContext.Set<T>()
                                                                                                  .ToListAsync(cancellationToken);

        public void Create(T entity)
        {
            entity.DateCreated = DateTimeOffset.UtcNow;
            _dbContext.Add(entity);
        }

        public void Update(T entity)
        {
            entity.DateUpdated = DateTimeOffset.UtcNow;
            _dbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.DateDeleted = DateTimeOffset.UtcNow;
            _dbContext.Remove(entity);
        }
    }
}
