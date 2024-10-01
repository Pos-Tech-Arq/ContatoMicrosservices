using System.Net;
using FluentAssertions;
using Moq;
using Refit;
using Regiao.AntiCorruption.BrasilApiService;
using Regiao.AntiCorruption.BrasilApiService.Responses;
using Regiao.AntiCorruption.BrasilApiService.Services;

namespace Regiao.UnitTests.Services;

public class BuscaRegiaoServiceTests
{
    [Theory(DisplayName = "Retorna regiao com sucesso")]
    [InlineData("11")]
    public async Task BuscaRegiao_DeveRetornarComSucesso_QuandoCodigoValido(string ddd)
    {
        // Arrange
        var mockBrasilApi = new Mock<IBrasilApi>();
        var regiaoMockResponse = new RegiaoResponse
        {
            State = "SP",
            cities = new List<string> { "São Paulo", "Osasco" }
        };

        mockBrasilApi.Setup(b => b.BuscaRegiaoPorDdd(ddd)).ReturnsAsync(regiaoMockResponse);
        var regiaoService = new BuscaRegiaoService(mockBrasilApi.Object);

        // Act
        var regiao = await regiaoService.BuscaRegiao(ddd);

        // Assert
        regiao.Should().NotBeNull();
        regiao.Should().BeSameAs(regiaoMockResponse);
    }

    [Theory(DisplayName = "Retorna not found e mensagem de erro quando ddd inválido")]
    [InlineData("59")]
    public async Task BuscaRegiao_DeveRetornarNotFound_QuandoCodigoInvalido(string ddd)
    {
        // Arrange
        var brasilApi = new Mock<IBrasilApi>();
        var apiException = ApiException.Create(
            new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/ddd/v1/{ddd}"),
            HttpMethod.Get,
            new HttpResponseMessage(HttpStatusCode.NotFound),
            new RefitSettings()).Result;

        brasilApi.Setup(b => b.BuscaRegiaoPorDdd(ddd)).ReturnsAsync(() => throw apiException);
        var regiaoService = new BuscaRegiaoService(brasilApi.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => regiaoService.BuscaRegiao(ddd));
        Assert.Equal("Código de região inválido", exception.Message);
    }
}