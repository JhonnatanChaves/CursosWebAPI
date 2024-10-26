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
    }
}
