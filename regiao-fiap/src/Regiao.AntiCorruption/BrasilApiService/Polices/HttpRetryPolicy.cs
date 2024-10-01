using Polly;
using Polly.Extensions.Http;

namespace Regiao.AntiCorruption.BrasilApiService.Polices;

public static class HttpRetryPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(x => !x.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(3, retryAttempt)));
    }
}