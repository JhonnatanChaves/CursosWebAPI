using CursosWebApi.Domain.Helpers;
using CursosWebApi.Domain.Entities;

namespace CursosWebApi.Domain.Models
{
    public class TurmaDTO
    {
        public required string NomeTurma { get; set; }

        public required string Codigo { get; set; }

        public required string CargaHoraria { get; set; }

        public required ENiveisCurso Nivel { get; set; }

    }
}
