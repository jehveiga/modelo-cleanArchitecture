using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext DbContext;

        protected readonly DbSet<T> DbSet; // propriedade que facilita a chamada se necessário

        protected BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public async Task<T> Get(Guid id, CancellationToken cancellationToken) => await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


        public async Task<List<T>> GetAll(CancellationToken cancellationToken) => await DbSet.ToListAsync(cancellationToken);

        public void Create(T entity)
        {
            entity.DateCreated = DateTimeOffset.UtcNow;
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            entity.DateUpdated = DateTimeOffset.UtcNow;
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.DateDeleted = DateTimeOffset.UtcNow;
            DbSet.Remove(entity);
        }
    }
}
