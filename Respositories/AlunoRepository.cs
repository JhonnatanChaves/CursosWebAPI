using CursosWebApi.Entities;
using CursosWebApi.Infrastructure;
using CursosWebApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApi.Respositories
{
    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(CursosDbContext context) : base(context)
        {
        }

        public async Task<Aluno?> BuscarAlunoPorCPF(string cpf)
        {
            try
            {
                return await _context.Alunos.SingleOrDefaultAsync(aluno => aluno.Cpf == cpf);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<Aluno?> BuscarAlunoPorMatricula(string matricula)
        {
            try
            {
                return await _context.Alunos.FirstOrDefaultAsync(aluno => aluno.Matricula == matricula);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task CadastrarAluno(Aluno aluno)
        {
            try
            {
                await _context.Alunos.AddAsync(aluno);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Aluno>> ListarAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task AtualizarAluno(Aluno aluno)
        {
            try
            {
                _context.Update(aluno);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoverAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
