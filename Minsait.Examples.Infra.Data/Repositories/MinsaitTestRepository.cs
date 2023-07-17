#nullable disable

using Microsoft.EntityFrameworkCore;
using Minsait.Examples.Domain.Entities;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;
using Minsait.Examples.Infra.Data.Context;

namespace Minsait.Examples.Infra.Data.Repositories
{
    public class MinsaitTestRepository : IMinsaitTestRepository
    {
        private readonly MyContext _context;

        public MinsaitTestRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MinsaitTest entity)
        {
            await _context.Tests.AddAsync(entity);
        }

        public void Delete(long id)
        {
            var entity = _context.Tests.FirstOrDefault(x => x.Id == id);
            _context.Tests.Remove(entity);
        }

        public IQueryable<MinsaitTest> GetQueryable()
        {
            return _context.Tests.AsQueryable();
        }

        public async Task<MinsaitTest> GetByIdAsync(long id)
        {
            return await _context.Tests.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public void Update(MinsaitTest entity)
        {
            _context.Tests.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
