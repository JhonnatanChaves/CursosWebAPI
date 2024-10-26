using CursosWebApi.Entities;
using CursosWebApi.Models;

namespace CursosWebApi.Interfaces
{
    public interface IAlunoService
    {
        public Task<bool> CadastrarAluno(AlunoDTO alunoDTO, string codigoTurma);
        public Task<AlunoDTO?> BuscarAlunoPorCPF(string cpf);
        public Task<bool> MatricularAluno(string matricula, string codigoTurma);
        public Task<IEnumerable<AlunoDTO>> ListarAlunos();

        public Task<AlunoDTO?> BuscarAlunoPorMatricula(string matricula);

        public Task AtualizarAluno(AlunoDTO alunoDTO);

        public Task RemoverAluno(string matricula);
    }
}
