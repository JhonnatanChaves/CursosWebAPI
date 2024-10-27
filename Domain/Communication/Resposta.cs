namespace CursosWebApi.Domain.Communication
{
    public class Resposta
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public object? Objeto { get; set; }

        public Resposta(bool sucesso, string? mensagem, object? objeto)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Objeto = objeto;
        }
    }
}
