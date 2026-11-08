using Xunit;
using Moq;
using System.Threading.Tasks;
using BeaconSync.Application.Services;
using BeaconSync.Domain.Models;
using BeaconSync.Application.DTOs;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Tests.Unit;

public class RespostaAlertaServiceTests
{
    private readonly Mock<IRespostaAlertaRepository> _repositoryMock;
    private readonly RespostaAlertaService _service;

    public RespostaAlertaServiceTests()
    {
        _repositoryMock = new Mock<IRespostaAlertaRepository>();
        _service = new RespostaAlertaService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidRespostaAlerta_CallsRepositoryAddOnce()
    {
        // Arrange
        var dto = new RespostaAlertaDto
        {
            // Se a propriedade no seu DTO for diferente, ajuste aqui (ex: Mensagem, Texto, etc.)
            // Se usar a propriedade padrão da model, coloque o nome exato dela:
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<RespostaAlerta>()))
                       .ReturnsAsync(new RespostaAlerta { Id = 1 });

        // Act
        await _service.AddAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<RespostaAlerta>()), Times.Once);
    }
}