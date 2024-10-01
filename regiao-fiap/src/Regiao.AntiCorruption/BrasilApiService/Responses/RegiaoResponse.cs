using System.Text.Json.Serialization;

namespace Regiao.AntiCorruption.BrasilApiService.Responses;

public class RegiaoResponse
{
    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("cities")] public List<string> cities { get; set; }
}