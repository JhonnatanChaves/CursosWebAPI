﻿using CursosWebApi.Domain.Entities;
using CursosWebApi.Domain.Models;

namespace CursosWebApi.Domain.Respositories
{
    public interface ITurmaRepository
    {
        public Task CadastrarTurma(Turma turma);

        public Task<Turma?> BuscarTurmaPorCodigo(string codigo);

        public Task AtualizarTurma(Turma turma);

        public Task<List<Aluno>?> ListarAlunosPorTurma(string codigoTurma);

        public Task<List<Turma>> ListarTurmasDoAluno(Aluno aluno);

        public Task<List<Turma>> ListarTurmas();

        public Task RemoverTurma(Turma turma);

    }
}
