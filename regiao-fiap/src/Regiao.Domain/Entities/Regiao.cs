using Regiao.Domain.Contracts;

namespace Regiao.Domain.Entities;

public class Regiao : Entidade, IAggregateRoot
{
    public string Ddd { get; private set; }

    public string Estado { get; private set; }

    private readonly List<Cidade> _cidades;
    public IReadOnlyCollection<Cidade> Cidades => _cidades;

    // public Regiao(string ddd)
    // {
    //     Ddd = ddd;
    //     Id = Guid.NewGuid();
    // }

    public Regiao(string ddd, string estado)
    {
        Ddd = ddd;
        Estado = estado;
        _cidades = new List<Cidade>();
        Id = Guid.NewGuid();
    }

    public void AdicionaCidades(IEnumerable<Cidade> cidades)
    {
        _cidades.AddRange(cidades);
    }

    protected Regiao()
    {
        _cidades = new List<Cidade>();
    }
}