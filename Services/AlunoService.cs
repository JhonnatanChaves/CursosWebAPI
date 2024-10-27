using AutoMapper;
using CursosWebApi.Domain.Entities;
using CursosWebApi.Domain.Services;
using CursosWebApi.Domain.Models;
using CursosWebApi.Domain.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CursosWebApi.Domain.Communication;

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

        public async Task<Resposta> CadastrarAluno(AlunoDTO alunoDTO, string codigoTurma)
        {
            try
            {
                var alunoExiste = await _alunoRepository.BuscarAlunoPorCPF(alunoDTO.Cpf);

                if (alunoExiste != null)
                    return new Resposta(true, "O aluno já possui cadastro.", null);

                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigoTurma);

                if (turma == null)
                    return new Resposta(true, "Turma não encontrada.", null);

                if (turma.QtdAlunos < 5)
                {
                    var aluno = _mapper.Map<Aluno>(alunoDTO);

                    aluno.Turmas.Add(turma);
                    await _alunoRepository.CadastrarAluno(aluno);

                    turma.QtdAlunos++;
                    await _turmaRepository.AtualizarTurma(turma);

                    return new Resposta(true, "O aluno foi cadastrado.", alunoDTO); ;
                }

                return new Resposta(true, "Não foi possível caastrar o aluno, pois a turma já atingiu seu limite de matriculas.", null);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível cadastrar o aluno.", null);
            }
        }

        public async Task<Resposta> BuscarAlunoPorCPF(string cpf)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorCPF(cpf);

                if (aluno == null) return new Resposta(true, "Aluno não encontrado.", null);

                var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

                return new Resposta(true, "Busca efetuada com sucesso.", alunoDTO);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível realizar a busca", null);
            }
        }

        public async Task<Resposta> MatricularAluno(string matricula, string codigoTurma)
        {
            try
            {
                if (matricula.IsNullOrEmpty() || codigoTurma.IsNullOrEmpty())
                    return new Resposta(true, "Verifique se os campos de matrícula e código da turma estão corretos.", null);

                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(matricula);

                if (aluno == null) return new Resposta(true, "Aluno não existe", null);

                var turma = await _turmaRepository.BuscarTurmaPorCodigo(codigoTurma);
                if (turma == null) return new Resposta(true, "Turma não existe", null);


                var listaDeAlunos = await _turmaRepository.ListarAlunosPorTurma(codigoTurma);

                if (listaDeAlunos == null) return new Resposta(true, "Não existem alunos na turma", null);

                var verificaAlunoNaTurma = listaDeAlunos.Any(a => a.Matricula == aluno.Matricula);
                if (verificaAlunoNaTurma) return new Resposta(true, "O aluno já está matriculado.", null);

                turma.Alunos.Add(aluno);
                turma.QtdAlunos++;

                await _turmaRepository.AtualizarTurma(turma);

                return new Resposta(true, "Aluno matriculado com sucesso.", null); ;

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível matricular o aluno.", null);
            }
        }

        public async Task<Resposta> ListarAlunos()
        {
            try
            {
                var listaDeAlunos = await _alunoRepository.ListarAlunos();

                if (listaDeAlunos == null) return new Resposta(true, "Não existem alunos cadastrados.", null);

                return new Resposta(true, "Busca efetuada com sucesso.", listaDeAlunos);
            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível buscar os alunos.", null);
            }
        }

        public async Task<Resposta> BuscarAlunoPorMatricula(string matricula)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(matricula);

                if (aluno == null) return new Resposta(true, "Aluno não existe.", null);

                var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

                return new Resposta(true, "Busca efetuada com sucesso.", alunoDTO);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível buscar o aluno.", null);
            }
        }

        public async Task<Resposta> AtualizarAluno(AlunoDTO alunoDTO)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(alunoDTO.Matricula);

                if (aluno == null) return new Resposta(true, "Aluno não cadastrado", null);

                _mapper.Map(alunoDTO, aluno);

                await _alunoRepository.AtualizarAluno(aluno);

                return new Resposta(true, "Aluno atualizado com sucesso.", alunoDTO);

            }
            catch (Exception)
            {
                return new Resposta(false, "Não foi possível recuperar o aluno para atualização", null);
            }
        }

        public async Task<Resposta> RemoverAluno(string matricula)
        {
            try
            {
                var aluno = await _alunoRepository.BuscarAlunoPorMatricula(matricula);

                if (aluno == null) return new Resposta(true, "Aluno não cadastrado", null);

                else
                {
                    var turmas = await _turmaRepository.ListarTurmasDoAluno(aluno);

                    if (turmas == null) return new Resposta(true, "Aluno não está matriculado", null);

                    foreach (var turma in turmas)
                    {
                        turma.Alunos.Remove(aluno);
                        turma.QtdAlunos--;

                        await _turmaRepository.AtualizarTurma(turma);
                    }

                    var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

                    await _alunoRepository.RemoverAluno(aluno);


                    return new Resposta(true, "Aluno removido com sucesso", alunoDTO);

                }
            }
            catch (Exception)
            {
                return new Resposta(true, "Não foi possível remover o aluno", null);
            }
        }
    }
}
