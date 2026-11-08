namespace BeaconSync.Domain.Models;

public class PessoaVulneravel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string TipoVulnerabilidade { get; set; } = string.Empty;
    public string NecessidadeEspecial { get; set; } = string.Empty;

    public ICollection<Responsavel> Responsaveis { get; set; } = new List<Responsavel>();
    public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    public ICollection<RespostaAlerta> RespostasAlertas { get; set; } = new List<RespostaAlerta>();
}