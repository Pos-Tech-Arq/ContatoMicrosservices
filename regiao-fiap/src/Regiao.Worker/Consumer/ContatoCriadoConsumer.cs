using MassTransit;
using Message.Contato;
using Regiao.Domain.Command;
using Regiao.Domain.Contracts;

namespace Regiao.Worker.Consumer;

public class ContatoCriadoConsumer : IConsumer<ContatoCriado>
{
    private readonly ICriaRegiaoService _criaRegiaoService;

    public ContatoCriadoConsumer(ICriaRegiaoService criaRegiaoService)
    {
        _criaRegiaoService = criaRegiaoService;
    }

    public async Task Consume(ConsumeContext<ContatoCriado> context)
    {
        var message = context.Message;

        string ddd = message.Ddd;

        await _criaRegiaoService.Handle(new CriaRegiaoCommand(ddd));
    }
}