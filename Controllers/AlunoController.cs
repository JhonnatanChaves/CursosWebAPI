using CursosWebApi.Entities;
using CursosWebApi.Interfaces;
using CursosWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CursosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        [Route("cadastrarAluno")]
        public async Task<IActionResult> CadastrarAluno(AlunoDTO alunoDTO, string codigoTurma)
        {
            try
            {
                var status = await _alunoService.CadastrarAluno(alunoDTO, codigoTurma);

                if(status)
                    return Ok("Aluno Cadastrado");
                else 
                    return BadRequest("Aluno não cadastrado, verifique o CPF do aluno, código da turma e número limite de alunos por turma");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível cadastrar o aluno");
            }
        }

        [HttpPost]
        [Route("matricularAluno")]
        public async Task<IActionResult> MatricularAlunos(string matricula, string codigoTurma)
        {
            try
            {
                var status = await _alunoService.MatricularAluno(matricula, codigoTurma);

                if (status)
                    return Ok("Aluno Matriculado");
                else
                    return BadRequest("Aluno não matriculado. Verifique a matricula, código e limite de alunos da turma");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível matricular o aluno");
            }
        }

        [HttpGet]
        [Route("buscarAlunoPorCpf")]
        public async Task<IActionResult> BuscarAlunoPorCPF(string cpf)
        {
            try
            {
                var alunoDTO = await _alunoService.BuscarAlunoPorCPF(cpf);

                return Ok(alunoDTO);

            }
            catch (Exception)
            {
                return BadRequest("Não foi possível buscar os alunos");
            }
        }

        [HttpGet]
        [Route("buscarAlunoPorMatricula")]
        public async Task<IActionResult> BuscarAlunoPorMatricula(string matricula)
        {
            try
            {
                var alunoDTO = await _alunoService.BuscarAlunoPorMatricula(matricula);

                if(alunoDTO != null) return Ok(alunoDTO);

                return BadRequest("O aluno não encontrado, verifique sua matrícula ");

            }
            catch (Exception)
            {
                return BadRequest("Não foi possível buscar os alunos");
            }
        }

        [HttpGet]
        [Route("listarAlunos")]
        public async Task<IActionResult> BuscarAlunos()
        {
            try
            {
                var listaDeAlunos = await _alunoService.ListarAlunos();
                
                return Ok(listaDeAlunos);
                
            }catch (Exception)
            {
               return BadRequest("Não foi possível buscar os alunos");
            }
        }

        [HttpPut]
        [Route("atualizarAluno")]
        public async Task<IActionResult> EditarAluno(AlunoDTO alunoDTO)
        {
            try
            {
                await _alunoService.AtualizarAluno(alunoDTO);

                return Ok("As informações referentes ao aluno foram atualizadas");

            }
            catch (Exception)
            {
                return BadRequest("Não foi possível buscar os alunos");
            }
        }

        [HttpDelete]
        [Route("removerAluno")]
        public async Task<IActionResult> RemoverAluno(string matricula)
        {
            try
            {
                await _alunoService.RemoverAluno(matricula);
                return Ok("Aluno removido");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível remover o aluno");
            }
        } 
    }
}
