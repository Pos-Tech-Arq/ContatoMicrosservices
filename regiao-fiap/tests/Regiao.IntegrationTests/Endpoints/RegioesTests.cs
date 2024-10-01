using Microsoft.EntityFrameworkCore;
using Regiao.IntegrationTests.Factories;

namespace Regiao.IntegrationTests.Endpoints;

public class RegioesTests(RegiaoFactory factory) : BaseIntegrationTests(factory)
{
    [Theory(DisplayName = "Busca regiao por DDD com sucesso e salva informações no banco")]
    [InlineData("11")]
    [InlineData("21")]
    [InlineData("17")]
    public async Task RegisterRegiao_DeveRetornarComSucesso_QuandoDDDValido(string ddd)
    {
        // Act
        var response = await Client.PostAsync($"/api/v1/regioes?ddd={ddd}", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var regiao = DbContext.Regioes.Include(r => r.Cidades).FirstOrDefault(r => r.Ddd == ddd);
        regiao.Should().NotBeNull();
        regiao.Cidades.Should().NotBeNullOrEmpty();
    }
}