namespace BeaconSync.Application.DTOs;

public class PessoaVulneravelDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string TipoVulnerabilidade { get; set; } = string.Empty;
    public string NecessidadeEspecial { get; set; } = string.Empty;
}