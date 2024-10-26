using CursosWebApi.Entities;
using CursosWebApi.Interfaces;
using CursosWebApi.Models;
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
            try
            {
                await _turmaService.CadastrarTurma(turmaDTO);

                return Ok("Turma cadastrada.");
            }
            catch (Exception)
            {
                return BadRequest("Turma não cadastrada.");
            }
        }

        [HttpGet]
        [Route("buscarTurmaPorCodigo")]
        public async Task<IActionResult> BuscarTurmaPorCodigo(string codigo)
        {
            try
            {
                var turmaDTO = await _turmaService.BuscarTurmaPorCodigo(codigo);
                return Ok(turmaDTO);

            }catch (Exception)
            {
                return BadRequest("Turma não encontrada.");
            }
        }

        [HttpGet]
        [Route("listarTurmas")]
        public async Task<IActionResult> ListarTurmas()
        {
            try
            {
                var listaDeTurmas = await _turmaService.ListarTurmas();
                return Ok(listaDeTurmas);

            }
            catch (Exception)
            {
                return BadRequest("Turma não encontrada.");
            }
        }

        [HttpDelete]
        [Route("removerTurma")]
        public async Task<IActionResult> RemoverTurma(string codigoTurma)
        {
            try
            {
                await _turmaService.RemoverTurma(codigoTurma);

                return Ok("Turma excluída");

            }
            catch (Exception)
            {
                throw new Exception("Não foi possível obter a lista de turmas");
            }
        }


    }
}
