using CursosWebApi.Domain.Communication;
using CursosWebApi.Domain.Models;

namespace CursosWebApi.Domain.Services
{
    public interface ITurmaService
    {
        public Task<Resposta> CadastrarTurma(TurmaDTO turmaDTO);

        public Task<Resposta> BuscarTurmaPorCodigo(string codigo);

        public Task<Resposta> ListarTurmas();

        public Task<Resposta> RemoverTurma(string codigoTurma);
    }
}
