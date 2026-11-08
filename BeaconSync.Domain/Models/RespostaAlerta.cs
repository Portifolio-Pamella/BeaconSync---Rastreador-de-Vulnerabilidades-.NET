namespace BeaconSync.Domain.Models;

public class RespostaAlerta
{
    public int Id { get; set; }
    public int PessoaVulneravelId { get; set; }
    public PessoaVulneravel? PessoaVulneravel { get; set; }

    public int EnderecoOndeEstaId { get; set; }
    public string StatusSeguranca { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public DateTime DataResposta { get; set; } = DateTime.UtcNow;
}