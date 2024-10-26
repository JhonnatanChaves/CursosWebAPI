using CursosWebApi.Infrastructure;

namespace CursosWebApi.Persistence.Repositories
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
