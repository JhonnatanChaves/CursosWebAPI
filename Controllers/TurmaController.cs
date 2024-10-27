using CursosWebApi.Domain.Communication;
using CursosWebApi.Domain.Services;
using CursosWebApi.Domain.Models;
using CursosWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CursosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
       
        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost]
        [Route("cadastrarTurma")]
        public async Task<IActionResult> CadastrarTurma(TurmaDTO turmaDTO)
        {
            var resposta = await _turmaService.CadastrarTurma(turmaDTO);

            if(resposta.Sucesso)
                return Ok(resposta.Mensagem);

            return BadRequest(resposta.Mensagem);            
        }

        [HttpGet]
        [Route("buscarTurmaPorCodigo")]
        public async Task<IActionResult> BuscarTurmaPorCodigo(string codigo)
        {
            
            var resposta = await _turmaService.BuscarTurmaPorCodigo(codigo);

            if(resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta.Mensagem);
            
        }

        [HttpGet]
        [Route("listarTurmas")]
        public async Task<IActionResult> ListarTurmas()
        {           
            var resposta = await _turmaService.ListarTurmas();
            
            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta.Mensagem);

        }

        [HttpDelete]
        [Route("removerTurma")]
        public async Task<IActionResult> RemoverTurma(string codigoTurma)
        {
            var resposta = await _turmaService.RemoverTurma(codigoTurma);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta.Mensagem);

        }


    }
}
