using Xunit;
using Moq;
using System.Threading.Tasks;
using BeaconSync.Application.Services;
using BeaconSync.Domain.Models;
using BeaconSync.Application.DTOs;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Tests.Unit;

public class PessoaVulneravelServiceTests
{
    private readonly Mock<IPessoaVulneravelRepository> _repositoryMock;
    private readonly PessoaVulneravelService _service;

    public PessoaVulneravelServiceTests()
    {
        _repositoryMock = new Mock<IPessoaVulneravelRepository>();
        _service = new PessoaVulneravelService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidPessoa_CallsRepositoryAddOnce()
    {
        // Arrange
        var dto = new PessoaVulneravelDto
        {
            Nome = "João Silva"
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<PessoaVulneravel>())).Returns(Task.CompletedTask);

        // Act
        await _service.AddAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<PessoaVulneravel>()), Times.Once);
    }
}