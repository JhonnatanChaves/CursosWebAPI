using CursosWebApi.Domain.Entities;
using CursosWebApi.Domain.Respositories;
using CursosWebApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApi.Infrastructure.Persistence.Repositories
{
    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(CursosDbContext context) : base(context)
        {
        }

        public async Task<Aluno?> BuscarAlunoPorCPF(string cpf)
        {
            return await _context.Alunos.SingleOrDefaultAsync(aluno => aluno.Cpf == cpf);

        }

        public async Task<Aluno?> BuscarAlunoPorMatricula(string matricula)
        {
            return await _context.Alunos.FirstOrDefaultAsync(aluno => aluno.Matricula == matricula);

        }

        public async Task CadastrarAluno(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Aluno>> ListarAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task AtualizarAluno(Aluno aluno)
        {
            _context.Update(aluno);
            await _context.SaveChangesAsync();

        }

        public async Task RemoverAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

        }
    }
}
