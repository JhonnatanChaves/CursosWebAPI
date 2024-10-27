using AutoMapper;
using CursosWebApi.Domain.Entities;
using CursosWebApi.Domain.Services;
using CursosWebApi.Domain.Models;
using CursosWebApi.Domain.Respositories;
using CursosWebApi.Domain.Communication;

namespace CursosWebApi.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly IMapper _mapper;
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(IMapper mapper, ITurmaRepository turmaRepository)
        {
            _mapper = mapper;
            _turmaRepository = turmaRepository;
        }

        public async Task<Resposta> CadastrarTurma(TurmaDTO turmaDTO)
        {
            try
            {
                var turma = _mapper.Map<Turma>(turmaDTO);
                await _turmaRepository.CadastrarTurma(turma);

                return new Resposta(true, "Turma Cadastrada.", turma);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível cadastrar a turma.", null);
            }
        }

        public async Task<Resposta> BuscarTurmaPorCodigo(string codigo)
        {
            try
            {
                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigo);

                var turmaDTO = _mapper.Map<TurmaDTO>(turma);

                return new Resposta(true, "Busca efetuada com sucesso.", turmaDTO);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível obter a turma", null);
            }
        }

        public async Task<Resposta> ListarTurmas()
        {
            try
            {
                var turmas = await _turmaRepository.ListarTurmas();

                var listaTurmas = _mapper.Map<List<TurmaDTO>>(turmas);

                if (listaTurmas == null) return new Resposta(true, "lista vazia", null);

                return new Resposta(true, "lista obtida com sucesso", listaTurmas);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível obter a lista", null);
            }
        }

        public async Task<Resposta> RemoverTurma(string codigoTurma)
        {
            try
            {
                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigoTurma);

                if (turma == null)
                    return new Resposta(true, "Turma não encontrada.", turma);

                if (turma.Alunos.Count == 0)
                {
                    await _turmaRepository.RemoverTurma(turma);
                    return new Resposta(true, "Turma removida com sucesso.", turma);
                }

                return new Resposta(true, "Remoção negada! Não é possível remover uma turma com alunos matriculados.", turma);
            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível remover a turma.", null);
            }
        }
    }
}
