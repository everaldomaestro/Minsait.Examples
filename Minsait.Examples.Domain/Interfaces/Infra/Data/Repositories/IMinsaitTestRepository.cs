using Minsait.Examples.Domain.Entities;

namespace Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IMinsaitTestRepository : IMinsaitTestQueries
    {
        Task AddAsync(MinsaitTest entity);
        void Update(MinsaitTest entity);
        void Delete(long id);        
        Task<int> SaveChangesAsync();
    }
}
