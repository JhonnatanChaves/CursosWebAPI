using CursosWebApi.Domain.Services;
using CursosWebApi.Domain.Models;
using CursosWebApi.Domain.Entities;
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
            
            var resposta = await _alunoService.CadastrarAluno(alunoDTO, codigoTurma);

            if(resposta.Sucesso)
                return Ok(resposta.Mensagem);
            else 
                return BadRequest(resposta.Mensagem);
         
        }

        [HttpPost]
        [Route("matricularAluno")]
        public async Task<IActionResult> MatricularAluno(string matricula, string codigoTurma)
        {           
            var resposta = await _alunoService.MatricularAluno(matricula, codigoTurma);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta.Mensagem);

        }

        [HttpGet]
        [Route("buscarAlunoPorCpf")]
        public async Task<IActionResult> BuscarAlunoPorCPF(string cpf)
        {            
            var resposta = await _alunoService.BuscarAlunoPorCPF(cpf);

            if(resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta.Mensagem);
            
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
            var resposta = await _alunoService.ListarAlunos();

            if(resposta.Sucesso) return Ok(resposta);
                           
            return BadRequest(resposta.Mensagem);
            
        }

        [HttpPut]
        [Route("atualizarAluno")]
        public async Task<IActionResult> EditarAluno(AlunoDTO alunoDTO)
        {
            try
            {
                var resposta =  await _alunoService.AtualizarAluno(alunoDTO);

                if (resposta.Sucesso) return Ok(resposta);

                return BadRequest(resposta.Mensagem);

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

            var resposta = await _alunoService.RemoverAluno(matricula);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta.Mensagem);
        } 
    }
}
