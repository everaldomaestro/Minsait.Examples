using Minsait.Examples.Domain.Entities;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;

namespace Minsait.Examples.Application.Queries
{
    public class MinsaitTestQueries : IMinsaitTestQueries
    {
        private readonly IMinsaitTestRepository _repository;

        public MinsaitTestQueries(IMinsaitTestRepository repository)
        {
            _repository = repository;
        }

        public async Task<MinsaitTest> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public IQueryable<MinsaitTest> GetQueryable()
        {
            return _repository.GetQueryable();
        }
    }
}
