namespace FaiscaSync.DTO
{
    public class CriarClienteDTO
    {
    // Cliente
    public string Nome { get; set; }
    public DateTime Datanasc { get; set; }
    public string Contato { get; set; }
    public string Nif { get; set; }

    // Morada
    public string Rua { get; set; }
    public string Numero { get; set;}
    public string? Andar { get; set;}

    // Código Postal
    public string CodigoPostal { get; set; }
    }   
}