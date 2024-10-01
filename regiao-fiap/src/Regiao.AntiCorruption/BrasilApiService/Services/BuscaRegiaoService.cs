using Refit;
using Regiao.AntiCorruption.BrasilApiService.Responses;

namespace Regiao.AntiCorruption.BrasilApiService.Services;

public class BuscaRegiaoService : IBuscaRegiaoService
{
    private readonly IBrasilApi _brasilApi;

    public BuscaRegiaoService(IBrasilApi brasilApi)
    {
        _brasilApi = brasilApi;
    }

    public async Task<RegiaoResponse> BuscaRegiao(string ddd)
    {
        try
        {
            return await _brasilApi.BuscaRegiaoPorDdd(ddd);
        }
        catch (ApiException e)
        {
            throw new HttpRequestException("Código de região inválido");
        }
    }
}