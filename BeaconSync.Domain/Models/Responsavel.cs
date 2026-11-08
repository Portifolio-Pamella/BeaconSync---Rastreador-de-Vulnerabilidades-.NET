namespace BeaconSync.Domain.Models;

public class Responsavel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool MoraComVulneravel { get; set; }

    public string LogradouroRes { get; set; } = string.Empty;
    public string NumeroRes { get; set; } = string.Empty;
    public string BairroRes { get; set; } = string.Empty;
    public string CidadeRes { get; set; } = string.Empty;

    public int PessoaVulneravelId { get; set; }
    public PessoaVulneravel? PessoaVulneravel { get; set; }
}