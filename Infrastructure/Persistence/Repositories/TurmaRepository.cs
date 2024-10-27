using CursosWebApi.Domain.Entities;
using CursosWebApi.Domain.Respositories;
using CursosWebApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApi.Infrastructure.Persistence.Repositories
{
    public class TurmaRepository : BaseRepository, ITurmaRepository
    {
        public TurmaRepository(CursosDbContext context) : base(context)
        {
        }

        public async Task AtualizarTurma(Turma turma)
        {
            _context.Update(turma);
            await _context.SaveChangesAsync();

        }

        public async Task<Turma?> BuscarTurmaPorCodigo(string codigo)
        {
            return await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(turma => turma.Codigo == codigo);
        }

        public async Task CadastrarTurma(Turma turma)
        {
            await _context.Turmas.AddAsync(turma);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Aluno>?> ListarAlunosPorTurma(string codigoTurma)
        {

            var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Codigo == codigoTurma);

            return turma?.Alunos.ToList();

        }

        public async Task<List<Turma>> ListarTurmas()
        {
            return await _context.Turmas.ToListAsync();

        }

        public async Task<List<Turma>> ListarTurmasDoAluno(Aluno aluno)
        {
            return await _context.Turmas.Where(t => t.Alunos.Contains(aluno)).ToListAsync();

        }

        public async Task RemoverTurma(Turma turma)
        {
            _context.Remove(turma);
            await _context.SaveChangesAsync();

        }
    }
}
