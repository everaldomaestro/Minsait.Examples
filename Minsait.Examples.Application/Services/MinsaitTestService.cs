using Minsait.Examples.Domain.Entities;
using Minsait.Examples.Domain.Interfaces.Application.Services;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;

namespace Minsait.Examples.Application.Services
{
    public class MinsaitTestService : IMinsaitTestService
    {
        private readonly IMinsaitTestRepository _repository;

        public MinsaitTestService(IMinsaitTestRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(MinsaitTest entity)
        {
            entity.Creation = DateTime.Now;

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            _repository.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<MinsaitTest> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public IQueryable<MinsaitTest> GetQueryable()
        {
            return _repository.GetQueryable();
        }

        public async Task UpdateAsync(MinsaitTest entity)
        {
            entity.Update = DateTime.Now;

            _repository.Update(entity);

            await _repository.SaveChangesAsync();
        }

        public bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
    }
}
