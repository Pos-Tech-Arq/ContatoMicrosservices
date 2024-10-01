using Moq;
using Regiao.AntiCorruption.BrasilApiService.Responses;
using Regiao.AntiCorruption.BrasilApiService.Services;
using Regiao.Domain.Command;
using Regiao.Domain.Contracts;
using Regiao.Domain.Services;

namespace Regiao.UnitTests.Services;

public class CriaRegiaoServiceTests
{
    private readonly Mock<IRegiaoRepository> _regiaoRepositoryMock;
    private readonly Mock<IBuscaRegiaoService> _buscaRegiaoServiceMock;
    private readonly CriaRegiaoService _criaRegiaoService;

    public CriaRegiaoServiceTests()
    {
        _regiaoRepositoryMock = new Mock<IRegiaoRepository>();
        _buscaRegiaoServiceMock = new Mock<IBuscaRegiaoService>();
        _criaRegiaoService = new CriaRegiaoService(_regiaoRepositoryMock.Object, _buscaRegiaoServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateRegiao_WhenValidCommandIsProvided()
    {
        // Arrange
        var criaRegiaoCommand = new CriaRegiaoCommand("11");

        _regiaoRepositoryMock.Setup(r => r.GetByDdd(It.IsAny<string>()))
            .ReturnsAsync((Domain.Entities.Regiao)null);
        _buscaRegiaoServiceMock.Setup(b => b.BuscaRegiao(It.IsAny<string>())).ReturnsAsync(new RegiaoResponse
        {
            State = "SP",
            cities = new List<string> { "São Paulo", "Osasco" }
        });
        _regiaoRepositoryMock.Setup(r => r.Create(It.IsAny<Domain.Entities.Regiao>()))
            .Returns(Task.CompletedTask);

        // Act
        await _criaRegiaoService.Handle(criaRegiaoCommand);

        // Assert
        _regiaoRepositoryMock.Verify(
            r => r.Create(It.Is<Domain.Entities.Regiao>(r => r.Ddd == "11" && r.Estado == "SP")), Times.Once);
        _buscaRegiaoServiceMock.Verify(b => b.BuscaRegiao("11"), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenRegiaoCreationFails()
    {
        // Arrange
        var criaRegiaoCommand = new CriaRegiaoCommand("11");

        _regiaoRepositoryMock.Setup(r => r.GetByDdd(It.IsAny<string>()))
            .ReturnsAsync((Domain.Entities.Regiao)null);
        _buscaRegiaoServiceMock.Setup(b => b.BuscaRegiao(It.IsAny<string>())).ThrowsAsync(new Exception("Error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _criaRegiaoService.Handle(criaRegiaoCommand));
    }
}