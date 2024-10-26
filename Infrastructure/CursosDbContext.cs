using CursosWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApi.Infrastructure
{
    public class CursosDbContext: DbContext
    {

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        public CursosDbContext(DbContextOptions options) : base(options) { }

    }
}
