using Toledo.Core.Interfaces;
using Toledo.Infrastructure.Data;

namespace Toledo.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ToledoContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ToledoContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}
