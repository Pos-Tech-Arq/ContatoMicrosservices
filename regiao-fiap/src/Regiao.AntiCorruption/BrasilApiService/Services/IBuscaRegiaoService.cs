using Regiao.AntiCorruption.BrasilApiService.Responses;

namespace Regiao.AntiCorruption.BrasilApiService.Services;

public interface IBuscaRegiaoService
{
    Task<RegiaoResponse> BuscaRegiao(string ddd);
}