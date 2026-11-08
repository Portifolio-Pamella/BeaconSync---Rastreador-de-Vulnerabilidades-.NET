namespace BeaconSync.Application.DTOs;

public class EnderecoDto
{
    public int Id { get; set; }
    public int PessoaVulneravelId { get; set; }
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Distrito { get; set; } = string.Empty;
    public string TipoImovel { get; set; } = string.Empty;
    public bool TemLajeParaHelicoptero { get; set; }
    public int Andar { get; set; }
    public bool EEnderecoAtual { get; set; }
}