using AutoMapper;
using CursosWebApi.Entities;
using CursosWebApi.Interfaces;
using CursosWebApi.Models;
using CursosWebApi.Respositories;

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

        public async Task CadastrarTurma(TurmaDTO turmaDTO)
        {
            try
            {
                var turma = _mapper.Map<Turma>(turmaDTO);
                await _turmaRepository.CadastrarTurma(turma);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TurmaDTO?> BuscarTurmaPorCodigo(string codigo)
        {
            try
            {
                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigo);
            
                return  _mapper.Map<TurmaDTO>(turma);                                       

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TurmaDTO>> ListarTurmas()
        {
            try
            {
                var turmas = await _turmaRepository.ListarTurmas();

                return _mapper.Map<List<TurmaDTO>>(turmas);

            }
            catch (Exception)
            {
                throw new Exception("Não foi possível obter a lista de turmas");
            }
        }

        public async Task RemoverTurma(string codigoTurma)
        {
            try
            {
                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigoTurma);

                if (turma != null && turma.Alunos.Count() == 0)
                {
                    await _turmaRepository.RemoverTurma(turma);
                }                
                
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível obter a lista de turmas");
            }
        }
    }
}
