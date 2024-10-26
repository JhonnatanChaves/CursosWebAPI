using CursosWebApi.Entities;
using CursosWebApi.Models;

namespace CursosWebApi.Respositories
{
    public interface IAlunoRepository
    {
        public Task<IEnumerable<Aluno>> ListarAlunos();
        public Task<Aluno?> BuscarAlunoPorCPF(string cpf);
        public Task<Aluno?> BuscarAlunoPorMatricula(string matricula);
        public Task CadastrarAluno(Aluno aluno);

        public Task AtualizarAluno(Aluno aluno);

        public Task RemoverAluno(Aluno aluno);

    }
}
