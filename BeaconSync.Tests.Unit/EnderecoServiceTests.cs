using Xunit;
using Moq;
using System.Threading.Tasks;
using BeaconSync.Application.Services;
using BeaconSync.Domain.Models;
using BeaconSync.Application.DTOs;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Tests.Unit;

public class EnderecoServiceTests
{
    private readonly Mock<IEnderecoRepository> _repositoryMock;
    private readonly EnderecoService _service;

    public EnderecoServiceTests()
    {
        _repositoryMock = new Mock<IEnderecoRepository>();
        _service = new EnderecoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidEndereco_CallsRepositoryAddOnce()
    {
        // Arrange
        var dto = new EnderecoDto
        {
            Logradouro = "Rua Pacobaiba"
        };

        // Simula o retorno do repositório retornando a entidade criada ou o ID
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Endereco>()))
                       .ReturnsAsync(new Endereco { Id = 1 });

        // Act
        await _service.AddAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Endereco>()), Times.Once);
    }
}