using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories
{
    // Classe Responsável por controlar a transmissão de dados para o Data Base, centralizando suas transmissões de dados somente nela
    // usando sempre o mesmo contexto por meio do tempo de vida Scoped definido na injeção pela a mesma operação usada nas outras classes que utilizam DbContext
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public UnitOfwork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
