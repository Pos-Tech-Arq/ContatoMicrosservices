using Microsoft.EntityFrameworkCore;
using Regiao.Domain.Contracts;
using Regiao.Infra.Contexts;
using RegiaoEntitie = Regiao.Domain.Entities;

namespace Regiao.Infra.Repositories;

public class RegiaoRepository : IRegiaoRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private DbSet<RegiaoEntitie.Regiao> _dbSet;

    public RegiaoRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Regioes;
    }

    public async Task Create(RegiaoEntitie.Regiao regiao)
    {
        await _dbSet.AddAsync(regiao);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<RegiaoEntitie.Regiao?> GetByDdd(string ddd)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Ddd == ddd);
    }
}
