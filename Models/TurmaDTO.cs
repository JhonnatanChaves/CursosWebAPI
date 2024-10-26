using CursosWebApi.Entities;

namespace CursosWebApi.Models
{
    public class TurmaDTO
    {
        public required string NomeTurma { get; set; }

        public required string Codigo { get; set; }

        public required string CargaHoraria { get; set; }

        public required string Nivel { get; set; }
        
    }
}
