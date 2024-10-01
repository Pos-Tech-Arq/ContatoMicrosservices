using Regiao.AntiCorruption.BrasilApiService.Services;
using Regiao.Domain.Command;
using Regiao.Domain.Contracts;
using RegiaoEntitie = Regiao.Domain.Entities;

namespace Regiao.Domain.Services;

public class CriaRegiaoService(IRegiaoRepository regiaoRepository, IBuscaRegiaoService buscaRegiaoService)
    : ICriaRegiaoService
{
    public async Task Handle(CriaRegiaoCommand command)
    {
        var regiaoBrasilService = await buscaRegiaoService.BuscaRegiao(command.Ddd);
        var regiao = new RegiaoEntitie.Regiao(command.Ddd, regiaoBrasilService.State);
        regiao.AdicionaCidades(regiaoBrasilService.cities.Select(c => new RegiaoEntitie.Cidade(c)));
        await regiaoRepository.Create(regiao);
    }
}