using CursosWebApi.Infrastructure.Persistence.Context;

namespace CursosWebApi.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly CursosDbContext _context;

        public BaseRepository(CursosDbContext context)
        {
            _context = context;
        }
    }
}
