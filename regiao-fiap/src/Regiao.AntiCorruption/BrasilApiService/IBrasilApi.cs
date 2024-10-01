using Refit;
using Regiao.AntiCorruption.BrasilApiService.Responses;

namespace Regiao.AntiCorruption.BrasilApiService;

public interface IBrasilApi
{
    [Get("/api/ddd/v1/{ddd}")]
    Task<RegiaoResponse> BuscaRegiaoPorDdd(string ddd);
}