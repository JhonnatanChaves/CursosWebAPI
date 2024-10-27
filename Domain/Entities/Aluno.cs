namespace CursosWebApi.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Matricula { get; set; }

        public required string Cpf { get; set; }

        public required string Email { get; set; }

        public required List<Turma> Turmas { get; set; } = new List<Turma>();

    }
}
