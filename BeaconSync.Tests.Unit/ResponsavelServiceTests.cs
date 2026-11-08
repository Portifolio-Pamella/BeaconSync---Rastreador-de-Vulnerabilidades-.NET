using Xunit;
using Moq;
using System.Threading.Tasks;
using BeaconSync.Application.Services;
using BeaconSync.Domain.Models;
using BeaconSync.Application.DTOs;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Tests.Unit;

public class ResponsavelServiceTests
{
    private readonly Mock<IResponsavelRepository> _repositoryMock;
    private readonly ResponsavelService _service;

    public ResponsavelServiceTests()
    {
        _repositoryMock = new Mock<IResponsavelRepository>();
        _service = new ResponsavelService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidResponsavel_CallsRepositoryAddOnce()
    {
        // Arrange
        var dto = new ResponsavelDto
        {
            Nome = "Maria Oliveira"
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Responsavel>()))
                       .ReturnsAsync(new Responsavel { Id = 1 });

        // Act
        await _service.AddAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Responsavel>()), Times.Once);
    }
}