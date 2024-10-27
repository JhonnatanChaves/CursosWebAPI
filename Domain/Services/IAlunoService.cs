using CursosWebApi.Domain.Communication;
using CursosWebApi.Domain.Models;

namespace CursosWebApi.Domain.Services
{
    public interface IAlunoService
    {
        public Task<Resposta> CadastrarAluno(AlunoDTO alunoDTO, string codigoTurma);
        public Task<Resposta> BuscarAlunoPorCPF(string cpf);
        public Task<Resposta> MatricularAluno(string matricula, string codigoTurma);
        public Task<Resposta> ListarAlunos();

        public Task<Resposta> BuscarAlunoPorMatricula(string matricula);

        public Task<Resposta> AtualizarAluno(AlunoDTO alunoDTO);

        public Task<Resposta> RemoverAluno(string matricula);
    }
}
