using RegiaoEntities = Regiao.Domain.Entities;

namespace Regiao.Domain.Contracts;

public interface IRegiaoRepository
{
    Task Create(RegiaoEntities.Regiao contato);

    Task<RegiaoEntities.Regiao?> GetByDdd(string ddd);
}
