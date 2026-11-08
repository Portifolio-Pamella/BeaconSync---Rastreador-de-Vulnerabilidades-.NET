namespace BeaconSync.Application.DTOs;

public class VoluntarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Especialidade { get; set; } = string.Empty;
    public bool DisponivelParaAjuda { get; set; }
}