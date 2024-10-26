using CursosWebApi.Entities;
using CursosWebApi.Infrastructure;
using CursosWebApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApi.Respositories
{
    public class TurmaRepository : BaseRepository, ITurmaRepository
    {
        public TurmaRepository(CursosDbContext context) : base(context)
        {
        }

        public async Task AtualizarTurma(Turma turma)
        {
            try
            {
                _context.Update(turma);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        public async Task<Turma?> BuscarTurmaPorCodigo(string codigo)
        {
            try
            {
                return await _context.Turmas.FirstOrDefaultAsync(turma => turma.Codigo == codigo);
                               
            }catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task CadastrarTurma(Turma turma)
        {
            try
            {
                await _context.Turmas.AddAsync(turma);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Aluno>?> ListarAlunosPorTurma(string codigoTurma)
        {
            try
            {     
                var turma = await _context.Turmas
                                    .Include(t => t.Alunos)
                                    .FirstOrDefaultAsync(t => t.Codigo == codigoTurma);

                return turma?.Alunos.ToList();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
