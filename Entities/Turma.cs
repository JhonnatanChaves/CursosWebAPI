namespace CursosWebApi.Entities
{
    public class Turma
    {
        public int Id { get; set; }

        public required string NomeTurma { get; set; }

        public required string Codigo {  get; set; }

        public required string CargaHoraria { get; set; }

        public required string Nivel { get; set;}

        public required int QtdAlunos {  get; set; }

        public required List<Aluno> Alunos { get; set; } = new List<Aluno>();
    
    }
}
