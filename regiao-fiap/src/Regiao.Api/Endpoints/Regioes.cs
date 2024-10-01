using Microsoft.AspNetCore.Mvc;
using Regiao.Domain.Command;
using Regiao.Domain.Contracts;

namespace Regiao.Api.Endpoints;

public static class Regioes
{
    public static void RegisterRegiaoEndpoints(this IEndpointRouteBuilder routes)
    {
        var regiaoRoute = routes.MapGroup("/api/v1/regioes");

        regiaoRoute.MapPost("",
                async (ICriaRegiaoService criaContatoService, [FromQuery] string? ddd) =>
                {
                    var command = new CriaRegiaoCommand(ddd);
                    await criaContatoService.Handle(command);

                    return TypedResults.NoContent();
                })
            .WithName("CriaRegiao")
            .WithOpenApi()
            .AddFluentValidationFilter();
    }
}