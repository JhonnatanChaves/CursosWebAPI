using CursosWebApi.Entities;

namespace CursosWebApi.Respositories
{
    public interface ITurmaRepository
    {
        public Task CadastrarTurma(Turma turma);
        
        public Task<Turma?> BuscarTurmaPorCodigo(string codigo);

        public Task AtualizarTurma(Turma turma);

        public Task<List<Aluno>?> ListarAlunosPorTurma(string codigoTurma);

    }
}
