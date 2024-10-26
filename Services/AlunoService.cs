using AutoMapper;
using CursosWebApi.Entities;
using CursosWebApi.Interfaces;
using CursosWebApi.Models;
using CursosWebApi.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CursosWebApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IMapper _mapper;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public AlunoService(IAlunoRepository alunoRepository, IMapper mapper, ITurmaRepository turmaRepository)
        {
            _mapper = mapper;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
        }

        public async Task<AlunoDTO?> BuscarAlunoPorCPF(string cpf)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorCPF(cpf);

                return _mapper.Map<AlunoDTO>(aluno); ;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CadastrarAluno(AlunoDTO alunoDTO, string codigoTurma)
        {
            try
            {
                var alunoExiste = await _alunoRepository.BuscarAlunoPorCPF(alunoDTO.Cpf);

                if (alunoExiste != null) return false;

                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigoTurma);

                if (turma != null && turma.QtdAlunos < 5)
                {
                    var aluno = _mapper.Map<Aluno>(alunoDTO);

                    aluno.Turmas.Add(turma);
                    await _alunoRepository.CadastrarAluno(aluno);

                    turma.QtdAlunos++;
                    await _turmaRepository.AtualizarTurma(turma);

                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> MatricularAluno(string matricula, string codigoTurma)
        {
            try
            {
                if (matricula.IsNullOrEmpty() || codigoTurma.IsNullOrEmpty()) return false;

                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(matricula);
                if (aluno == null) return false;

                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigoTurma);
                if (turma == null) return false;


                var listaDeAlunos = await _turmaRepository.ListarAlunosPorTurma(codigoTurma);

                var verificaAlunoNaTurma = listaDeAlunos.Any(a => a.Matricula == aluno.Matricula);
                if (verificaAlunoNaTurma) return false;

                turma.Alunos.Add(aluno);
                turma.QtdAlunos++;

                await _turmaRepository.AtualizarTurma(turma);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<AlunoDTO>> ListarAlunos()
        {
            throw new NotImplementedException();
        }

        public async Task<AlunoDTO?> BuscarAlunoPorMatricula(string matricula)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(matricula);

                return _mapper.Map<AlunoDTO>(aluno);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AtualizarAluno(AlunoDTO alunoDTO)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(alunoDTO.Matricula);

                if (aluno == null) return;

                _mapper.Map(alunoDTO,aluno);

                await _alunoRepository.AtualizarAluno(aluno);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoverAluno(string matricula)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(matricula);

                if (aluno == null) throw new Exception("Aluno não existe");

                else
                {
                    var turmas = await _turmaRepository.ListarTurmasDoAluno(aluno);

                    foreach (var turma in turmas)
                    {
                        turma.Alunos.Remove(aluno);
                        turma.QtdAlunos--;

                        await _turmaRepository.AtualizarTurma(turma);
                    }

                    await _alunoRepository.RemoverAluno(aluno);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
