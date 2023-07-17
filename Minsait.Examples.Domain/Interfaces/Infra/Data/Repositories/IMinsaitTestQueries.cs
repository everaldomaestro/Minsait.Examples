using Minsait.Examples.Domain.Entities;

namespace Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IMinsaitTestQueries
    {
        Task<MinsaitTest> GetByIdAsync(long id);
        IQueryable<MinsaitTest> GetQueryable();
    }
}
