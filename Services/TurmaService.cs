using AutoMapper;
using CursosWebApi.Entities;
using CursosWebApi.Infrastructure.Repositories;
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
    }
}
