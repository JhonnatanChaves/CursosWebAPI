using CursosWebApi.Entities;
using CursosWebApi.Models;

namespace CursosWebApi.Interfaces
{
    public interface ITurmaService
    {
        public Task CadastrarTurma(TurmaDTO turmaDTO);

        public Task<TurmaDTO?> BuscarTurmaPorCodigo(string codigo);

        public Task<List<TurmaDTO>> ListarTurmas();

        public Task RemoverTurma(string codigoTurma);
    }
}
