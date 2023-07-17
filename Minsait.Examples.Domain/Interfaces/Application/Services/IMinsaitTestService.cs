using Minsait.Examples.Domain.Entities;

namespace Minsait.Examples.Domain.Interfaces.Application.Services
{
    public interface IMinsaitTestService
    {
        Task AddAsync(MinsaitTest entity);
        Task UpdateAsync(MinsaitTest entity);
        Task DeleteAsync(long id);
        Task<MinsaitTest> GetByIdAsync(long id);
        IQueryable<MinsaitTest> GetQueryable();
        bool IsLeapYear(int year);
    }
}
