using Xunit;
using Moq;
using System.Threading.Tasks;
using BeaconSync.Application.Services;
using BeaconSync.Domain.Models;
using BeaconSync.Application.DTOs;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Tests.Unit;

public class VoluntarioServiceTests
{
    private readonly Mock<IVoluntarioRepository> _repositoryMock;
    private readonly VoluntarioService _service;

    public VoluntarioServiceTests()
    {
        _repositoryMock = new Mock<IVoluntarioRepository>();
        _service = new VoluntarioService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidVoluntario_CallsRepositoryAddOnce()
    {
        // Arrange
        var dto = new VoluntarioDto
        {
            Nome = "Pamella Christiny",
            Email = "pamella@beaconsync.com"
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Voluntario>()))
                       .ReturnsAsync(new Voluntario { Id = 1 });

        // Act
        await _service.AddAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Voluntario>()), Times.Once);
    }
}