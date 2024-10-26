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
        public async Task<IActionResult> CadastrarAlunos(AlunoDTO alunoDTO, string codigoTurma)
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
    }
}
