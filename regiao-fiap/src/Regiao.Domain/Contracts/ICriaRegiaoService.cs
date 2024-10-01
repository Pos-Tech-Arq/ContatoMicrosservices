using Regiao.Domain.Command;

namespace Regiao.Domain.Contracts;

public interface ICriaRegiaoService
{
    Task Handle(CriaRegiaoCommand command);
}
