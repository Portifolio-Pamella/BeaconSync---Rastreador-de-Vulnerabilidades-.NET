namespace BeaconSync.Application.DTOs;

public class ResponsavelDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool MoraComVulneravel { get; set; }
    public int PessoaVulneravelId { get; set; }
}