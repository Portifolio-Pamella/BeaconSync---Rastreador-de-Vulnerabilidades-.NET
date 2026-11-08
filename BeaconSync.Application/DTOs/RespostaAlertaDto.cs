namespace BeaconSync.Application.DTOs;

public class RespostaAlertaDto
{
    public int Id { get; set; }
    public int PessoaVulneravelId { get; set; }
    public int EnderecoOndeEstaId { get; set; }
    public string StatusSeguranca { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public DateTime DataResposta { get; set; }
}