namespace FaiscaSync.Models
{
    public class ResultadoOperacao
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }

        public static ResultadoOperacao Ok(string? msg = null) => new() { Sucesso = true, Mensagem = msg };
        public static ResultadoOperacao Falha(string msg) => new() { Sucesso = false, Mensagem = msg };
    }
}
